## What is this?

A repackaging of the old [crockford-base32](https://www.nuget.org/packages/crockford-base32) nuget package to support `.NET Standard 1.3`.

## Why does it exist?

The [crockford-base32](https://www.nuget.org/packages/crockford-base32) nuget package does not support `.NET Core` or `.NET Standard`.

## What changes did you make?

* Targeted `.NET Standard 1.3` 
* Migrated the unit tests to use `xUnit`

## How do I use it?

Install the [crockford-base32-core](https://www.nuget.org/packages/crockford-base32-core/) package via nuget:

```
Install-Package crockford-base32-core -Version 1.1.0
```

## How do I build it?

### Visual Studio 2017

Open `CrockfordBase32.sln` and compile.

### FAKE

* Ensure you have [.NET Core SDK 2.1.302](https://www.microsoft.com/net/download/dotnet-core/2.1) or higher installed
* Ensure you have [FAKE](https://fake.build/) installed:

  ```
  dotnet tool install fake-cli -g
  ```
* From the root directory on the command line, run `fake build`


## The original README follows:

A .NET encoder/decoder implementation of http://www.crockford.com/wrmg/base32.html

**Only installable via NuGet: http://nuget.org/List/Packages/crockford-base32** (You all use NuGet by now, right?)

Great for building hashes into URLs.

Resilient to humans:

* No crazy characters or keyboard gymnastics
* Totally case insensitive
* 0, O and o all decode to the same thing
* 1, I, i, L and l all decode to the same thing
* Doesn’t use U, so 519,571 encodes to FVCK instead
* Optional check digit on the end

Handles any ulong from 0 all the way through to 18,446,744,073,709,551,615.

 **Number** | **Encoded** | **Encoded with optional check digit**
--- | --- | ---
1 | 1 | 11
194 | 62 |629
456,789 | 1CKE |1CKEM
398,373 | C515 | C515Z
3,838,385,658,376,483 | 3D2ZQ6TVC93 | 3D2ZQ6TVC935
18,446,744,073,709,551,615 | FZZZZZZZZZZZZ | FZZZZZZZZZZZZB
