#!/usr/bin/env bash

set -e

readonly THIS_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
readonly THIS_FILE="$THIS_DIR/$(basename "${BASH_SOURCE[0]}")"

artifactsDir="$THIS_DIR/artifacts"

if [ -d artifactsDir ]; then
  rm -R artifactsDir
fi

build_number=${TRAVIS_BUILD_NUMBER:=1}

dotnet restore --verbosity normal /property:BuildNumber=$build_number

dotnet build --configuration Release --verbosity normal /property:BuildNumber=$build_number

dotnet pack --configuration Release --output "$artifactsDir" --no-build --verbosity normal /property:BuildNumber=$build_number
