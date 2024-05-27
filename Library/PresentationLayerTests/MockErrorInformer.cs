using Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerTests
{
    internal class MockErrorInformer : IErrorInformer
    {
        public string RecentMessage;

        public MockErrorInformer()
        {
            RecentMessage = string.Empty;
        }

        public void InformError(string message)
        {
            RecentMessage = message;
        }

        public void InformSuccess(string message)
        {
            RecentMessage = message;
        }

        public string GetRecentMessage()
        {
            return RecentMessage;
        }
    }
}
