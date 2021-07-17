#!/usr/bin/env bash

function get_out {
	if [[ "$#" -lt "3" ]]; then
		echo "Usage: $0 <target_platform> <target_project> <base_directory>" 
		exit 1
	fi
	local TARGET_PLATFORM="$1"
	local TARGET_PROJECT="$2"
	local BASE_DIR="$3"

	local TARGET_OUT="${TARGET_PROJECT/VideoDuplicateFinder/VDF}"
	local TARGET_OUT="${BASE_DIR}/${TARGET_OUT}-${TARGET_PLATFORM}"
	echo "$TARGET_OUT"
}

function bundle_ffmpeg {
	local USAGE="Usage: bundle_ffmpeg <target_platform> <target_folder_out> <ffmeg_parent_dir>"
	if [[ "$#" -lt "3" ]] || [[ -z "$1" ]] || [[ -z "$2" ]] || [[ -z "$3" ]]; then
		echo "$USAGE"
		exit 1
	fi
	local TARGET_PLATFORM="$1"
	local TARGET_OUT="$2"
	local FFMPEG_PARENT="$3"

	echo "${TARGET_PLATFORM}.FFMPEG: ====================================================="

	FFMPEG_DIR="$FFMPEG_PARENT/ffmpeg/${TARGET_PLATFORM}" 
	if [[ -d "$FFMPEG_DIR" ]]; then
		echo "${TARGET_PLATFORM}.FFMPEG: Found ${FFMPEG_DIR} copying => ${TARGET_OUT}/bin"

		mkdir -p "${TARGET_OUT}/bin/"
		cp ${FFMPEG_DIR}/*.* "${TARGET_OUT}/bin/"
	else
		echo "${TARGET_PLATFORM}.FFMPEG: No ${FFMPEG_DIR}"
	fi
}

function publish {
	if [[ "$#" -lt "3" ]]; then
		echo "Usage: $0 <target_platform> <target_project> <base_directory>" 
		exit 1
	fi

	local TARGET_PLATFORM="$1"
	local TARGET_PROJECT="$2"
	local BASE_DIR="$3"

	local TARGET_OUT="$(get_out $TARGET_PLATFORM $TARGET_PROJECT $BASE_DIR)"

	dotnet publish -c Release --self-contained -f netcoreapp3.1 \
		-r "$TARGET_PLATFORM" \
		-o "$TARGET_OUT" \
		"$TARGET_PROJECT"

	bundle_ffmpeg "$TARGET_PLATFORM" "$TARGET_OUT" "$BASE_DIR"
}

# Publish for windows

PLATFORM=win-x64
PROJECTS=("VideoDuplicateFinder.Windows" "VideoDuplicateFinder.Web" "VideoDuplicateFinder.Console")
BASE_DIR="Releases"

rm -r $BASE_DIR/VDF*

for project in ${PROJECTS[@]}; do
	echo "========== ${project}-${PLATFORM} ============================"
	publish "$PLATFORM" "$project" "$BASE_DIR"
done

# Publish for linux

PLATFORM=linux-x64
PROJECTS=("VideoDuplicateFinderLinux" "VideoDuplicateFinder.Web" "VideoDuplicateFinder.Console")

for project in ${PROJECTS[@]}; do
	echo "========== ${project}-${PLATFORM} ============================"
	publish "$PLATFORM" "$project" "$BASE_DIR"
done

# Zip each project
find "$BASE_DIR" -maxdepth 1 -type d -name 'VDF*' -execdir '/bin/sh' '-c' 'echo "{} => {}.zip" ; zip -qr "{}.zip" "{}"' ';'
