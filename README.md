# signalr-hangfire
Signalr notifications with hangfire background processing sample

I'm trying to create a sample where there are the following components/features:

* A hangfire server OWIN self-hosted from a Windows Service
* SignalR notifications when jobs are completed

I should note that the trick to this is that this sample works without a backplane, meaning it's basically IPC on a single machine (using AppDomain).
