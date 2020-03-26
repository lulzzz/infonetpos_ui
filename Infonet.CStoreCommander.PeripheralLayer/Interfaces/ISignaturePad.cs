using System;

namespace Infonet.CStoreCommander.PeripheralLayer.Interfaces
{
    public interface ISignaturePad : IDisposable
    {
        void Initialize(string signatureFileDirectory);

        string Text();

        bool IsProcessed();

        bool IsSignatureEmpty();

        void Clear();

        string Accept();

        string FinalizeSignature();
    }
}
