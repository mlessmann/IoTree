#!/bin/bash

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd ${DIR}/../src

nuget restore
xbuild /p:Configuration=Release IoTree.sln

if [ "$1" = "run" ]
then
	cd IoTree.Server
	sudo xsp
fi
