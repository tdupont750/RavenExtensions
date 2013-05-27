using System;
using Raven.Client;

namespace Raven.Extensions.Tests.RavenIndex
{
    public interface IRavenIndexData
        : IDisposable
    {
        IDocumentStore DocumentStore { get; }
    }
}