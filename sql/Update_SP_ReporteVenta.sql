-- Ejemplo: rehacer la parte relevante del SP para usar dbo.DetallesVenta
ALTER PROCEDURE dbo.SP_ReporteVenta
    @IdVenta INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        dv.Id_venta,
        dv.Id_producto,
        ISNULL(p.Nombre_producto, '') AS Nombre_producto,
        dv.Cantidad,
        dv.Precio_unitario,
        dv.Total
    FROM dbo.DetallesVenta AS dv
    LEFT JOIN dbo.Productos AS p
        ON p.Id_producto = dv.Id_producto
    WHERE dv.Id_venta = @IdVenta;
END;
GO