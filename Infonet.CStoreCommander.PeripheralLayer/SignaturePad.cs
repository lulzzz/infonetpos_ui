using System;
using Infonet.CStoreCommander.PeripheralLayer.Interfaces;
using PeripheralsComponent;

namespace Infonet.CStoreCommander.PeripheralLayer
{
    public class SignaturePad : ISignaturePad
    {
        private SignaturePadServer _signaturePad;

        public SignaturePad()
        {
            _signaturePad = new SignaturePadServer();
        }

        public string Accept()
        {
            return _signaturePad.Accept();
        }

        public void Clear()
        {
            _signaturePad.Clear();
        }

        public void Dispose()
        {
            _signaturePad.Dispose();
        }

        public string FinalizeSignature()
        {
            return _signaturePad.FinalizeSignature();
        }

        public void Initialize(string signatureFileDirectory)
        {
            _signaturePad.Initialize(signatureFileDirectory);
        }

        public bool IsProcessed()
        {
            return _signaturePad.IsProcessed();
        }

        public bool IsSignatureEmpty()
        {
            return _signaturePad.IsSignatureEmpty();
        }

        public string Text()
        {
            return _signaturePad.Text();
        }
    }
}
