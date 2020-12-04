@echo off
rem -- open port (first argument passed to batch script)
netsh advfirewall firewall add rule name="21port" dir=in action=Block protocol=TCP localport=21