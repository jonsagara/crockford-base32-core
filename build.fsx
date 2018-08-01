#r "paket:
nuget Fake.IO.FileSystem
nuget Fake.DotNet.Cli
nuget Fake.Core.Target //"
#load "./.fake/build.fsx/intellisense.fsx"

#if !FAKE
  #r "netstandard"
#endif

open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO


// Targets
Target.create "Clean" (fun _ ->
    Shell.cleanDir "./artifacts/"
)

Target.create "BuildProject" (fun _ ->
    DotNet.build (fun opt -> { opt with Configuration = DotNet.BuildConfiguration.Release }) ""
)

Target.create "RunTests" (fun _ ->
    DotNet.test (fun opt -> { opt with Configuration = DotNet.BuildConfiguration.Release }) @".\src\CrockfordBase32.Tests.Core\CrockfordBase32.Tests.Core.csproj"
)

Target.create "CreateNuGetPackage" (fun _ ->

    let setOptions (opt:DotNet.PackOptions) =
        // Set dotnet pack configuration properties
        let opt = { 
            opt with 
                Configuration = DotNet.BuildConfiguration.Release
                NoBuild = true
                OutputPath = Some "../../artifacts/" }

        // Pass custom parameters to dotnet
        opt.WithCommon (fun o -> { o with CustomParams = Some "--include-symbols --no-restore" })

    DotNet.pack setOptions "./src/CrockfordBase32/CrockfordBase32.csproj"
)


// Dependencies
"Clean"
    ==> "BuildProject"
    ==> "RunTests"
    ==> "CreateNuGetPackage"

// Start build
Target.runOrDefault "CreateNuGetPackage"
