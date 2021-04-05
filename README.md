# Jyobot
## About
This is a Twitch chatbot that I made for my own needs, but other people are welcome to use it as well. It depends on [TwitchLib](https://github.com/TwitchLib/TwitchLib) to handle communication with Twitch APIs, so be forewarned.

## Features
* Random quote generation

## Quick Start
To get started, you need to add a file called `configuration.json` to the directory with the application with the following structure:

```json
{
    "username": "<twitch username for the bot>",
    "token": "<oauth token from https://twitchapps.com/tmi/>",
    "channel": "<channel to join>"
}
```

Once that's done, you should be able to just edit the `quotes.json` file and add quotes as you desire, run the bot, and it will respond to users who type `!quote` with a random quote from the file.

As long as you already have .NET installed, you can run the bot from the command line with `dotnet run`.