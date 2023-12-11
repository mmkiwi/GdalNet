// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;

using MMKiwi.GdalNet.SampleData;

namespace Benchmark;

[Config(typeof(Config))]
public partial class GdalBenchmarks
{
    [GlobalSetup]
    public void GlobalSetup()
    {
        FileName = Path.GetTempFileName();
        using FileStream fs = File.OpenWrite(FileName);
        fs.Write(TestData.Geopackage.Data);
    }

    public string FileName { get; set; } = "";

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        File.Delete(FileName);
    }

    private class Config : ManualConfig
    {
        public Config()
        {
            var fastJob = Job.LongRun
                .WithIterationCount(1)
                .WithLaunchCount(1)
                .WithWarmupCount(1);
            AddJob(fastJob.WithPlatform(Platform.X64).WithRuntime(NativeAotRuntime.Net80));
            AddJob(fastJob.WithPlatform(Platform.X86).WithRuntime(CoreRuntime.Core80));
        }
    }
}
