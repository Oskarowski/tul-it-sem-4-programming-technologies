#!/bin/bash

if [ $# -eq 0 ]; then
    echo "Usage: $0 <number_of_runs>"
    exit 1
fi

number_of_runs=$1

# Validate if the provided argument is a positive integer
if ! [[ "$number_of_runs" =~ ^[0-9]+$ ]] || [ "$number_of_runs" -lt 1 ]; then
    echo "Error: <number_of_runs> must be a positive integer."
    exit 1
fi

# Define the output file
output_file="test_results.txt"

# Clear the output file
> "$output_file"

# Run dotnet test <number_of_runs> times
for ((i=1; i<=$number_of_runs; i++))
do
    echo "Test Run $i" >> "$output_file"
    dotnet test >> "$output_file"
    echo "======================================================================================================================================================" >> "$output_file"
done

echo "Test results saved to $output_file"
