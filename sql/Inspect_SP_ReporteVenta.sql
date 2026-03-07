-- Ver texto del SP
EXEC sp_helptext 'SP_ReporteVenta';

-- Confirmar BD actual y listar tablas con "Detalle"
SELECT DB_NAME() AS CurrentDB, USER_NAME() AS CurrentUser;
SELECT TABLE_SCHEMA, TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME LIKE '%Detalle%';