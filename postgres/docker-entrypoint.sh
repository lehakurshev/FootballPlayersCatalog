#!/bin/bash
# Start postgres
/usr/local/bin/postgres -p 8080 &
# Start pgadmin
/entrypoint.sh
wait
