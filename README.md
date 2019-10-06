# Plugable Mvc
A lightweight, flexible, and extensible modular framework for building .Net Core Mvc applications

## Introduction
Plugable Mvc is a free and open-source framework that makes it easy to build modular .Net Core Mvc applications.  
This is a very lightweight framework built on top of [Razor Class Libraries](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/ui-class?view=aspnetcore-2.2&tabs=visual-studio "Razor Class Libraries")

This framework will allow you to build an Mvc application based on plugins.  A Plugin consists of a separate .Net Core class library project that can contain Models, Views, Controllers, embedded static files, middleware components, or anything else.  The intent of Plugable Mvc is to be a very lightweight framework that just serves to make it easier to divide up an Mvc application into separate plugins that get added as [Application Parts](https://docs.microsoft.com/en-us/aspnet/core/mvc/advanced/app-parts?view=aspnetcore-2.2 "Application Parts") during startup, and that's it.  


## Core Packages

These are the core Nuget packages needed:

* PlugableMvc
* PlugableMvc.Hosting

#### PlugableMvc
Contains the base class needed to define a plugins and some interfaces needed to build a plugin.  Plugins are defined by adding a class 
to your class library that inherits from PlugableMvc.Plugins.PluginBase.cs.  This package also defines an interface called IPluginLocator.  This interface is used by the PluableMvc.Hosting package to discover the plugins that need to be added during application start.

#### PlugableMvc.Hosting
Contains the extension methods needed for an application to discover plugins during startup and add them as Application Parts.
This package also provides two implementations of IPluginLocator.cs that can be used to discover plugins in your application and add them.

## Additional Plugins

* PlugableMvc.Events

#### PlugableMvc.Events
A plugin you can add to any PlugableMvc application that adds a lightweight pubish/subscribe event system to your application.  
This allows the code in your application to define events and event handlers that can be broadcasted and handled.  This makes it easier 
for code in your plugins or application to communicate with one another, if you're into that sort of thing.  

## Examples
#### Coming Soon!
For now, you can look at the PlugableMvc.Everything plugin as an example of a plugin.
Also, you can refer to PlugableMvc.Host for an example of using PlugableMvc in a .Net Core 2.2 MVC application.
In the future there will be better examples.
