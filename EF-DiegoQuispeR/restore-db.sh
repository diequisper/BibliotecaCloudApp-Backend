#!/bin/bash
set -e

echo "Waiting for SQL Server to start..."

for i in {1..60}; do
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "${SA_PASSWORD}" -Q "SELECT 1" > /dev/null 2>&1 && break
    echo "SQL Server not ready yet... ($i)"
    sleep 2
done

echo "Restoring database..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "${SA_PASSWORD}" -Q "
RESTORE DATABASE [db_biblioteca]
FROM DISK = N'/var/opt/mssql/backups/biblioteca_db_bak.bak'
WITH MOVE 'db_biblioteca' TO '/var/opt/mssql/data/db_biblioteca.mdf',
     MOVE 'db_biblioteca_log' TO '/var/opt/mssql/data/db_biblioteca_log.ldf',
     REPLACE
"

echo "Database restored successfully."

