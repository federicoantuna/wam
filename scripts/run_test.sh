#!/usr/bin/bash
# Parameters - Solution
SOLUTION_FILE_NAME="WAM.sln"
COVERAGE_COVERLET_DIR=".coverage"
COVERAGE_REPORT_DIR=".coverage-report"

# Parameters - Coverlet
DATA_COLLECTOR_FORMAT="cobertura"
COVERLET_OUTPUT_FORMAT="cobertura"
COVERLET_OUTPUT_EXTENSION=".xml"
COLLECT="XPlat Code Coverage"
COVERAGE_FILE_NAME="coverage.$COVERLET_OUTPUT_FORMAT$COVERLET_OUTPUT_EXTENSION"

# Parameters - Report Generator
REPORT_TYPES="HTML;cobertura;"
HTML_REPORT_INDEX_FILE_NAME="index.html"

# Calculated Parameters - Solution - CHANGE WITH EXTREME CAUTION
SOLUTION_DIR="$(dirname $(cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd))"
SOLUTION="$SOLUTION_DIR/$SOLUTION_FILE_NAME"

# Calculated Parameters - Coverlet - CHANGE WITH EXTREME CAUTION
COVERAGE_RUN_IDENTIFIER=$(uuidgen)
COVERLET_OUTPUT="$SOLUTION_DIR/$COVERAGE_COVERLET_DIR/$COVERAGE_RUN_IDENTIFIER"

# Build and Test
dotnet test $SOLUTION --collect:"$COLLECT" --results-directory:"$COVERLET_OUTPUT" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format="$DATA_COLLECTOR_FORMAT"

# Calculated Parameters - Report Generator - CHANGE WITH EXTREME CAUTION
TARGET_DIR="$COVERAGE_REPORT_DIR/$COVERAGE_RUN_IDENTIFIER"
REPORT_HTML_FILE="$SOLUTION_DIR/$COVERAGE_REPORT_DIR/$COVERAGE_RUN_IDENTIFIER/$HTML_REPORT_INDEX_FILE_NAME"
COVERAGE_FILES=$(find $COVERLET_OUTPUT/**/* -maxdepth 1 | awk -vORS=";" '{ print $1 }' | sed "s/\;$/\n/")

# Generate Report
reportgenerator "-reports:$COVERAGE_FILES" "-targetdir:$TARGET_DIR" "-reporttypes:$REPORT_TYPES"

# Open Report
xdg-open $REPORT_HTML_FILE > /dev/null 2>&1