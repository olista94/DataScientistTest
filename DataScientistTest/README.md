# Transaction Categorizer

Este proyecto permite categorizar, como mínimo en 5 categorías diferentes, todas las transacciones presentes en la base de datos. 
Como resultado se espera obtener el listado de transacciones junto con la categoría asignada a cada una de ellas. De forma adicional, se requiere también una tabla resumen que indique la totalidad de transacciones asignadas a cada categoría.

Ordenar del 1 al 5 cada uno de los usuarios en función de su perfil crediticio, donde 1 indica el mejor pagador y 5 el peor. 
Como resultado se espera obtener la nota de cada usuario junto con una explicación razonada de los motivos de la misma. 

## Descripción

El programa toma un archivo de entrada en formato **Excel** (`.xlsx`) que debe encontrarse en la ruta del proyecto. Clasifica las transacciones en al menos cinco categorías diferentes basándose en la descripción y el monto, y genera un archivo de salida también en formato Excel con las transacciones categorizadas junto con una hoja adicional que contiene un resumen de las transacciones por categoría.

## Categorías

Las categorías implementadas son:

1. **Salary**: Transacciones relacionadas con salarios o nóminas.
2. **Own Transfers**: Transferencias entre cuentas propias.
3. **Third-Party Transfers**: Transferencias de terceros (Bizum, transferencias de amigos, etc.).
4. **Shopping**: Compras y reintegros.
5. **Bills/Services**: Pagos de servicios, cuotas y tarjetas.
6. **Interests/Fees**: Intereses o comisiones.
7. **Deposits**: Depósitos generales.
8. **Expenses**: Gastos generales.
9. **Others**: Transacciones que no se ajustan a las categorías anteriores.

## Requisitos

- [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0) o superior.
- Un archivo de entrada en formato `.xlsx` con las transacciones.

## Uso

### 1. Configuración del archivo de entrada y salida

En el archivo `Program.cs`, hay dos líneas que especifican las rutas del archivo de entrada y del archivo de salida. Estas deben ser ajustadas manualmente:

```csharp
string inputFilePath = @"ruta_del_proyecto\transactions.xlsx"; // Ruta al archivo de entrada
string outputFilePath = @"ruta_del_proyecto\Resultados_Text.xlsx"; // Ruta al archivo de salida
