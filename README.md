# Sitecore Host Messaging Plugins

This repository contains Sitecore Host plugins to connect to a RabbitMQ instance and subscribe to an exchange to process messages. This was originally used in a demo given at [SUGCON EU](https://www.sugcon.eu/) 2019 and at the [Atlanta Sitecore User Group](https://www.meetup.com/Atlanta-Sitecore/) in March 2019.

## Components 🧩

### `GC.Plugin.Messaging.Services`

This plugin adds RabbitMQ interfacing to the Sitecore Host application. On application load, this plugin creates a transient queue on the RabbitMQ server and opens up a receiver. A message processor can be specified in the config file.

### `GC.Plugin.Messaging.Alerts`

This plugin adds alert functionality to a web-based Sitecore Host instance. The message processor is configured in the `GC.Plugin.Messaging.Alerts.xml` config file and injection into the web application is done through a `TagHelper`.

## Usage 🙋‍♂️

There is a Cake script that will execute the packaging of the NuGet packages for the plugins.

Simply run `build.ps1` and the script will build and pack the packages for use with a Sitecore application.

If you'd like to output your NuGet packages to a specific directory, you can execute the Cake script like this:

`.\build.ps1 -ScriptArgs '-output="C:\output"'`

## Disclaimer ⚠

This is only intended as a proof-of-concept and does not contain production-ready code. Please don't treat it as such. 😎