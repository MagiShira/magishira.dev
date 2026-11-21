#!/bin/sh
# Publishes HEAD and swaps it in on the VM. Builds from a clean export, not the working tree, so a running `dotnet watch` is left alone.
set -eu
here=$(dirname "$0")
[ -f "$here/.env" ] && . "$here/.env" # DEPLOY_HOST, kept out of git
host=${1:-${DEPLOY_HOST:?set DEPLOY_HOST in deploy/.env, or pass the SSH host as the first argument}}
tmp=$(mktemp -d)
trap 'rm -rf "$tmp"' EXIT

git -C "$here/.." archive HEAD | tar -x -C "$tmp"
dotnet publish "$tmp/Magishira.csproj" -c Release -o "$tmp/out" -p:UseAppHost=false
tar --no-xattrs -C "$tmp/out" -cz . | ssh "$host" 'rm -rf /var/lib/magishira/app.new && mkdir -p /var/lib/magishira/app.new && tar -xz -C /var/lib/magishira/app.new \
  && rm -rf /var/lib/magishira/app && mv /var/lib/magishira/app.new /var/lib/magishira/app && systemctl restart magishira'
