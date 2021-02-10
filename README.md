# AR Portfolio 
###### - A Unity C# Application intended to allow its users to create and prepare a series of assets to present in an AR setting -

## ARFolio: Main Application
*An interactive AR portfolio application for students, artists and work.*

Will elaborate once I start working on it.

## ARFolioBundler: Companion Application
*To prepare assets for users during runtime.*

This application requires the use of Editor specific functions after the main application has been built and while it runs on
the user's device. While testing in the UnityEditor itself, there won't be a problem as it will compile with the Editor assembly 
which is where the `BuildPipeline` class is found. 

The aim is to essentially have users of the main application provide, edit and create their assets before uploading it directly 
to the companion application where those user-defined assets are prepared into a form that Unity Players can read and extract from,
regardless of how recent the Bundle was created. That's a bit of a mouthful.

> TL;DR: Need Editor functions during runtime when Editor isn't available. Upload to application that can use Editor functions,
provide finished product back to main application for use.

###### Batch Script:
- [x] On startup, check for existing Editor
- [x] Close if necessary
- [x] Get access to current Unity Editor executable
- [x] Get access to project location
- [x] Run our Unity Application in batchmode

###### Create Folio:
- [x] Cancel the script if it's been open in Windows Editor (otherwise it will build on startup regardless)
- [ ] Specify asset path
- [ ] Check for new and incoming folders
- [ ] Only bundle those that are completely uploaded 
- [x] Go through all files in folder
- [x] Validate for file types
- [ ] Validate for more file types and maybe make the code a lot nicer somehow
- [x] Set Bundle name and variant
- [x] Build set up Bundle to output path (for Android)
- [ ] Provide main application with ability to download Bundle

