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
}


PLATFORM=win-x64
PROJECTS=("VideoDuplicateFinder.Windows" "VideoDuplicateFinder.Web" "VideoDuplicateFinder.Console")
BASE_DIR="Releases"

rm -r $BASE_DIR/VDF*

for project in ${PROJECTS[@]}; do
	echo "Building $PLATFORM $project"
	TARGET_OUT="$(get_out $TARGET_PLATFORM $TARGET_PROJECT $BASE_DIR)"
	publish "$PLATFORM" "$project" "$BASE_DIR"

	if [[ -d "$BASE_DIR/ffmpeg" ]]; then
		mkdir -p "${TARGET_OUT}/bin/"
		cp ${BASE_DIR}/ffmpeg/x64/*.* "${TARGET_OUT}/bin/"
		
	fi

done


PLATFORM=linux-x64
PROJECTS=("VideoDuplicateFinderLinux" "VideoDuplicateFinder.Web" "VideoDuplicateFinder.Console")

for project in ${PROJECTS[@]}; do
	publish "$PLATFORM" "$project" "$BASE_DIR"
done

