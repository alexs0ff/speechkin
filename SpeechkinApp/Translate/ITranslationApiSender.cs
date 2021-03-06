﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpeechkinApp.Translate
{
    public interface ITranslationApiSender
    {
        Task<HttpResponseMessage> Send(HttpRequestMessage message, CancellationToken token);
    }
}
