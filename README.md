nomvc-todoapp
=============

Todo app for Naked Objects MVC, a simplified port of the Apache Isis "todo app".

Also includes cross-origin (CORS) configuration, and with the Restful Objects API enabled (under "/rest/").

For Visual Studio 2013.

Steps are:
- fork this repository in github
- clone the repo down to your PC
- import the solution into Visual Studio
- restore nugetPackages (in Package manager console)
- optionally, update App/App_Start/CorsConfig.cs to specify allowed cross-origin access.
- Build > Rebuild Solution

You should then be able to run the app
- Debug > Start Debugging (F5).
