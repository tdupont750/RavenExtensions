using System;
using System.Linq;
using Raven.Abstractions.Data;
using Raven.Abstractions.Logging;
using Raven.Bundles.Replication.Plugins;
using Raven.Json.Linq;

namespace Raven.Extensions
{
    /// <summary>
    /// Updated to RavenDB 2.0 from:
    /// https://gist.github.com/jtbennett
    /// </summary>
    public class LastInWinsReplicationConflictResolver : AbstractDocumentReplicationConflictResolver
    {
        private readonly ILog _log = LogManager.GetCurrentClassLogger();

        public override bool TryResolve(
            string id,
            RavenJObject metadata,
            RavenJObject document,
            JsonDocument existingDoc,
            Func<string, JsonDocument> getDocument)
        {
            if (ExistingDocShouldWin(metadata, existingDoc))
            {
                ReplaceValues(metadata, existingDoc.Metadata);
                ReplaceValues(document, existingDoc.DataAsJson);
                _log.Debug("Replication conflict for '{0}' resolved by choosing existing document.", id);
            }
            else
            {
                _log.Debug("Replication conflict for '{0}' resolved by choosing inbound document.", id);
            }

            return true;
        }

        private static bool ExistingDocShouldWin(RavenJObject newMetadata, JsonDocument existingDoc)
        {
            if (existingDoc == null ||
                ExistingDocHasConflict(existingDoc) ||
                ExistingDocIsOlder(newMetadata, existingDoc))
            {
                return false;
            }

            return true;
        }

        private static bool ExistingDocHasConflict(JsonDocument existingDoc)
        {
            return existingDoc.Metadata[Constants.RavenReplicationConflict] != null;
        }

        private static bool ExistingDocIsOlder(RavenJObject newMetadata, JsonDocument existingDoc)
        {
            var newLastModified = GetLastModified(newMetadata);

            if (!existingDoc.LastModified.HasValue ||
                newLastModified.HasValue &&
                existingDoc.LastModified <= newLastModified)
            {
                return true;
            }

            return false;
        }

        private static DateTime? GetLastModified(RavenJObject metadata)
        {
            var lastModified = metadata[Constants.LastModified];

            return (lastModified == null)
                ? new DateTime?()
                : lastModified.Value<DateTime?>();
        }

        private static void ReplaceValues(RavenJObject target, RavenJObject source)
        {
            var targetKeys = target.Keys.ToArray();
            foreach (var key in targetKeys)
            {
                target.Remove(key);
            }

            foreach (var key in source.Keys)
            {
                target.Add(key, source[key]);
            }
        }
    }
}
