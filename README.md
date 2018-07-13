> **2018-07-12**: Imported from https://archive.codeplex.com/?p=crockfordbase32 and modernized.
> 
> Original repository description follows.

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
