#!/bin/bash

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd ${DIR}/../src

nuget restore
xbuild /p:Configuration=Release IoTree.sln

sudo IoTree.Server/bin/Release/IoTree.Server.exe
