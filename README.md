# Sitecore Host Messaging Plugins

This repository contains sample plugins used in the Sitecore Host presentation given at SUGCON EU 2019.

## GC.Plugin.Messaging.Services

This plugin adds RabbitMQ interfacing to the Sitecore Host application. On application load, this plugin creates a transient queue on the RabbitMQ server and opens up a receiver. A message processor can be specified in the config file.

## GC.Plugin.Messaging.Alerts

This plugin adds alert functionality to a web-based Sitecore Host instance. The message processor is configured in the `GC.Plugin.Messaging.Alerts.xml` config file and injection into the web application is done through a `TagHelper`.
