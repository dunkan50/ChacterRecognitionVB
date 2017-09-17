Option Explicit On
Option Strict On

Imports System.Xml
Imports System.Xml.Serialization 'this is fro the imports seen at the end of the program
Imports System.IO

Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports Emgu.CV.Structure
Imports Emgu.CV.UI
Imports Emgu.CV.Util
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Class frmMain
    '''''MAIN VARIABLES''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Const MIN_CONTOUR_AREA As Integer = 100
    Const CHANGED_IMAGE_WIDTH As Integer = 20
    Const CHANGED_IMAGE_HEIGHT As Integer = 30


    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub trainingButton_Click(sender As Object, e As EventArgs) Handles trainingButton.Click
        Dim getChoosenFile As DialogResult
        getChoosenFile = ofDialog.ShowDialog()
        If (getChoosenFile <> DialogResult.OK Or ofDialog.FileName = "") Then  'if user chose cancle or filename is blank...
            choosenFile.Text = "file not opened"
            Return
        End If

        Dim numberTraingingImage As Mat

        Try
            numberTraingingImage = CvInvoke.Imread(ofDialog.FileName, LoadImageType.Color)
        Catch ex As Exception
            'when there are issues with the image
            choosenFile.Text = "Not able to open image" + ex.Message  'will show the error message from the error
            Return
        End Try

        If (numberTraingingImage Is Nothing) Then
            choosenFile.Text = "unable to open the file"
            Return

        End If

        choosenFile.Text = ofDialog.FileName 'update fro the file label

        Dim grayscaleImage As New Mat()
        Dim blurredImage As New Mat()
        Dim threshImage As New Mat()
        Dim copyOfThreshImage As New Mat()
        Dim contours As New VectorOfVectorOfPoint()

        Dim classificationMatrix As Matrix(Of Single)
        Dim trainingImagesMatrix As Matrix(Of Single)

        Dim flattenedTrainingImages As New Mat()

        Dim intValidChars As New List(Of Integer)(New Integer() {Asc("0"), Asc("1"), Asc("2"), Asc("3"), Asc("4"), Asc("5"), Asc("6"), Asc("7"), Asc("8"), Asc("9"),
                                                                  Asc("A"), Asc("B"), Asc("C"), Asc("D"), Asc("E"), Asc("F"), Asc("G"), Asc("H"), Asc("I"), Asc("J"),
                                                                  Asc("K"), Asc("L"), Asc("M"), Asc("N"), Asc("O"), Asc("P"), Asc("Q"), Asc("R"), Asc("S"), Asc("T"),
                                                                  Asc("U"), Asc("V"), Asc("W"), Asc("X"), Asc("Y"), Asc("Z")})
        CvInvoke.CvtColor(numberTraingingImage, grayscaleImage, ColorConversion.Bgr2Gray) 'grayscale conversion
        CvInvoke.GaussianBlur(grayscaleImage, blurredImage, New Size(5, 5), 0) 'blurring the given image
        CvInvoke.AdaptiveThreshold(blurredImage, threshImage, 255.0, AdaptiveThresholdType.GaussianC, ThresholdType.BinaryInv, 11, 2) 'converting the image to its threshhold image
        CvInvoke.Imshow("Thresh image", threshImage) 'shows the image being processed the thresh image

        copyOfThreshImage = threshImage.Clone() 'copy of the the image

        CvInvoke.FindContours(copyOfThreshImage, contours, Nothing, RetrType.External, ChainApproxMethod.ChainApproxSimple) 'gets only the external contours only

        Dim numberofTrainingSamples As Integer = contours.Size

        classificationMatrix = New Matrix(Of Single)(numberofTrainingSamples, 1) 'this is the classification of the data structure
        trainingImagesMatrix = New Matrix(Of Single)(numberofTrainingSamples, CHANGED_IMAGE_WIDTH * CHANGED_IMAGE_HEIGHT) 'keeps track of the roow we are in as the data is being analysied

        Dim trainingdataRowTOAdd As Integer = 0 'the smple will coresspond to a row in both the classificasion xml and the traingng images xml files

        For i As Integer = 0 To contours.Size - 1
            If (CvInvoke.ContourArea(contours(i)) > MIN_CONTOUR_AREA) Then
                Dim boundingrect As Rectangle = CvInvoke.BoundingRectangle(contours(i)) 'to get the charaters bounding rectangle
                CvInvoke.Rectangle(numberTraingingImage, boundingrect, New MCvScalar(0.0, 0.0, 255.0), 2) 'draws a red rect around each contour

                Dim copyofROIImage As New Mat(threshImage, boundingrect) 'gets data for the ROI image (Region Of Intrest)
                Dim ROIImage As Mat = copyofROIImage.Clone 'make a copy of image
                Dim ROIImageResized As New Mat() 'this is to alter the area of the image gotten

                CvInvoke.Resize(ROIImage, ROIImageResized, New Size(CHANGED_IMAGE_WIDTH, CHANGED_IMAGE_HEIGHT)) 'changed the height and width of the image
                CvInvoke.Imshow("ROI", ROIImage)
                CvInvoke.Imshow("ROI resized", ROIImageResized)
                CvInvoke.Imshow("Training images", numberTraingingImage) 'shows the image of the trainig images with the roi marked

                Dim charsInt As Integer = CvInvoke.WaitKey(0) 'pick up any key press
                If (charsInt = 27) Then
                    CvInvoke.DestroyAllWindows()
                    Return 'if the esc ket was pressed the program will exit

                ElseIf (intValidChars.Contains(charsInt)) Then
                    classificationMatrix(trainingdataRowTOAdd, 0) = Convert.ToSingle(charsInt) 'writes the classification chars to classifaction matrixes
                    'now we can add the training matrixes but we need to convert them allitle

                    Dim tempMatrix As Matrix(Of Single) = New Matrix(Of Single)(ROIImageResized.Size())
                    Dim reshapedTempMatrix As Matrix(Of Single) = New Matrix(Of Single)(1, CHANGED_IMAGE_WIDTH * CHANGED_IMAGE_HEIGHT) 'does the conversion

                    ROIImageResized.ConvertTo(tempMatrix, DepthType.Cv32F) 'changes the matrix image to singles of the same

                    For intRow As Integer = 0 To CHANGED_IMAGE_HEIGHT - 1 'flatten Matrix into one row by RESIZED_IMAGE_WIDTH * RESIZED_IMAGE_HEIGHT number of columns
                        For intColumn As Integer = 0 To CHANGED_IMAGE_WIDTH - 1
                            reshapedTempMatrix(0, (intRow * CHANGED_IMAGE_WIDTH) + intColumn) = tempMatrix(intRow, intColumn)
                        Next
                    Next

                    For intColumn As Integer = 0 To (CHANGED_IMAGE_WIDTH * CHANGED_IMAGE_HEIGHT) - 1
                        trainingImagesMatrix(trainingdataRowTOAdd, intColumn) = reshapedTempMatrix(0, intColumn)

                    Next
                    trainingdataRowTOAdd = trainingdataRowTOAdd + 1 'increases the column by one in the loop

                End If

            End If
        Next

        tbInfo.Text = tbInfo.Text + "finished the training " + vbCrLf + vbCrLf 'saves the classification to a file

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Dim xmlserial As XmlSerializer = New XmlSerializer(classificationMatrix.GetType)
        Dim writer As StreamWriter

        Try
            writer = New StreamWriter("classification.xml") 'this tries to open the classification files
        Catch ex As Exception
            tbInfo.Text = vbCrLf + tbInfo.Text + "unanble to open the classificaton file , error" + vbCrLf
            tbInfo.Text = tbInfo.Text + ex.Message + vbCrLf + vbCrLf

            Return

        End Try

        xmlserial.Serialize(writer, classificationMatrix) 'this is to save the training images
        writer.Close()

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        xmlserial = New XmlSerializer(trainingImagesMatrix.GetType)

        Try
            writer = New StreamWriter("images.xml") 'tries to open the images file
        Catch ex As Exception
            tbInfo.Text = vbCrLf + tbInfo.Text + "Not able to open file, error:" + vbCrLf
            tbInfo.Text = tbInfo.Text + ex.Message + vbCrLf + vbCrLf
            Return
        End Try

        xmlserial.Serialize(writer, trainingImagesMatrix)
        writer.Close()

        tbInfo.Text = vbCrLf + tbInfo.Text + "file writing done" + vbCrLf

        MsgBox("trainging complete, file writing too!!!")


    End Sub
End Class
