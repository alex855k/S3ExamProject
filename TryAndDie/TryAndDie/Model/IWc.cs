using System;
using System.Collections.Generic;
using DotnetSpider.Core;
using DotnetSpider.Core.Pipeline;
using DotnetSpider.Core.Processor;
using DotnetSpider.Core.Scheduler;
using DotnetSpider.Core.Selector;
using DotnetSpider.Core.Downloader;
using System.Text;

namespace TryAndDie.Model
{
    interface IWc
    {
        void Run(List<Uri> uriList);
        void PageProcessor<BaseProcessor>();

    }
}
