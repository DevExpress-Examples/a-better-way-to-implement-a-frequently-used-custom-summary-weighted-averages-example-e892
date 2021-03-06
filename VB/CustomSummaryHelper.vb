﻿Imports System
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid

Namespace WeightedAverages
    Public Class CustomSummaryHelper

        Public Shared Function GetWeightedAverage(ByVal view As GridView, ByVal weightField As String, ByVal valueField As String) As Object
            Const undefined As Decimal = 0
            If view Is Nothing Then
                Return undefined
            End If
            Dim weightCol As GridColumn = view.Columns(weightField)
            Dim valueCol As GridColumn = view.Columns(valueField)
            If weightCol Is Nothing OrElse valueCol Is Nothing Then
                Return undefined
            End If

            Try
                Dim totalWeight As Decimal = 0, totalValue As Decimal = 0
                For i As Integer = 0 To view.DataRowCount - 1
                    If view.IsNewItemRow(i) Then
                        Continue For
                    End If
                    Dim temp As Object
                    Dim weight, val As Decimal
                    temp = view.GetRowCellValue(i, weightCol)
                    weight = If(temp Is DBNull.Value OrElse temp Is Nothing, 0, Convert.ToDecimal(temp))
                    temp = view.GetRowCellValue(i, valueCol)
                    val = If(temp Is DBNull.Value OrElse temp Is Nothing, 0, Convert.ToDecimal(temp))

                    totalWeight += weight
                    totalValue += weight * val
                Next i
                If totalWeight = 0 Then
                    Return undefined
                End If
                Return totalValue / totalWeight
            Catch
                Return undefined
            End Try
        End Function
    End Class
End Namespace
