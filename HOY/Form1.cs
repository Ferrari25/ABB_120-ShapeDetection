using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace EmguCvProyect
{
    public partial class Form1 : Form
    {

        private VideoCapture capture;

        public Form1()
        {
            InitializeComponent();
            String win1 = " Window (Press any key to close)"; //nombre de la ventana 
            CvInvoke.NamedWindow(win1); //Crea una ventana con el nombre de win1

            capture = new VideoCapture();

            using (Mat frame = new Mat())
            using (UMat gray = new UMat())
            using (UMat cannyEdges = new UMat())
            using (Mat triangleRectangleImage = new Mat(frame.Size, DepthType.Cv8U, 3)) //image to draw triangles and rectangles on
            using (Mat lineImage = new Mat(frame.Size, DepthType.Cv8U, 3)) //image to drtaw lines on

            using (VideoCapture capture = new VideoCapture()) //VideoCapture() captura la imagen de la camara
                while (CvInvoke.WaitKey(1) == -1)
                {


                    //Abre la ventana del frame capturado por la cam
                    capture.Read(frame);

                    //convierte la imagen a escala de grices y filtra el ruido
                    CvInvoke.CvtColor(frame, gray, ColorConversion.Bgr2Gray);

                    // Dibuja las Etiquetas en la imagen en escala de grises
                    CvInvoke.PutText(gray, "Gray Image", new Point(20, 20),
                        FontFace.HersheyDuplex, 0.5, new MCvScalar(120, 120, 120));

                    //remueve el ruido de la imagen
                    CvInvoke.GaussianBlur(gray, gray, new Size(3, 3), 1);

                    // Crear una nueva imagen para los contornos
                    Mat contoursImage = new Mat(frame.Size, DepthType.Cv8U, 3);
                    contoursImage.SetTo(new MCvScalar(0)); // Fondo negro

                    // Convierte UMat a Bitmap
                    //Bitmap grayBitmap = gray.ToBitmap();

                    //LineSegment2D[] lines = CvInvoke.HoughLinesP(cannyEdges,1,Math.PI / 45.0,20,30,10); TRANSFORMADA DE HOUGH

                    //inicializar componentes/variables
                    double cannyThreshold = 180.0;

                    double cannyThresholdLinking = 120.0;
                    CvInvoke.Canny(gray, cannyEdges, cannyThreshold, cannyThresholdLinking);



                    //Encuentra Triangulos y Rectangulos
                    List<Triangle2DF> triangleList = new List<Triangle2DF>();
                    List<RotatedRect> boxList = new List<RotatedRect>(); //a box is a rotated rectangle
                    using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
                    {
                        CvInvoke.FindContours(cannyEdges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                        int count = contours.Size;
                        for (int i = 0; i < count; i++)
                        {
                            using (VectorOfPoint contour = contours[i])
                            using (VectorOfPoint approxContour = new VectorOfPoint())
                            {
                                CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);

                                if (CvInvoke.ContourArea(approxContour, false) > 250)//solo considera los contornos con area mayor a 25
                                {
                                    if (approxContour.Size == 3) //el contorno tiene 3 vertices ==> es un triangulo
                                    {
                                        Point[] pts = approxContour.ToArray();
                                        triangleList.Add(new Triangle2DF(
                                            pts[0],
                                            pts[1],
                                            pts[2]
                                        ));
                                    }
                                    else if (approxContour.Size == 4) //El contorno tiene 4 vertices
                                    {
                                        #region determine if all the angles in the contour are within [80, 100] degree
                                        bool isRectangle = true;
                                        Point[] pts = approxContour.ToArray();
                                        LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

                                        for (int j = 0; j < edges.Length; j++)
                                        {
                                            double angle = Math.Abs(
                                                edges[(j + 1) % edges.Length].GetExteriorAngleDegree(edges[j]));
                                            if (angle < 80 || angle > 100)
                                            {
                                                isRectangle = false;
                                                break;
                                            }
                                        }

                                        #endregion

                                        if (isRectangle) boxList.Add(CvInvoke.MinAreaRect(approxContour));
                                    }
                                }
                            }
                        }
                    }
                    //------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    // Crear una nueva imagen para los contornos



                    //------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //Dibuja Triangulos y Rectangulos en Imagen original y en Escala de Grices
                    triangleRectangleImage.SetTo(new MCvScalar(0));
                    foreach (Triangle2DF triangle in triangleList)
                    {
                        CvInvoke.Polylines(frame, Array.ConvertAll(triangle.GetVertices(), Point.Round),
                            true, new Bgr(Color.Green).MCvScalar, 2);

                        CvInvoke.Polylines(gray, Array.ConvertAll(triangle.GetVertices(), Point.Round),
                            true, new Bgr(Color.Green).MCvScalar, 2);

                        CvInvoke.Polylines(contoursImage, Array.ConvertAll(triangle.GetVertices(), Point.Round),
                            true, new Bgr(Color.LightCyan).MCvScalar, 2);
                    }

                    //------------------------------------------------------------------------------------------------------------------------------------------------------------------//

                    foreach (RotatedRect box in boxList)
                    {
                        CvInvoke.Polylines(frame, Array.ConvertAll(box.GetVertices(), Point.Round), true,
                            new Bgr(Color.DarkOrange).MCvScalar, 2);

                        CvInvoke.Polylines(gray, Array.ConvertAll(box.GetVertices(), Point.Round), true,
                            new Bgr(Color.DarkOrange).MCvScalar, 2);

                        CvInvoke.Polylines(contoursImage, Array.ConvertAll(box.GetVertices(), Point.Round), true,
                            new Bgr(Color.DeepPink).MCvScalar, 2);

                    }
                    //------------------------------------------------------------------------------------------------------------------------------------------------------------------//

                    //Dibuja un marco gris claro alrededor de la imagen
                    CvInvoke.Rectangle(frame,
                        new Rectangle(Point.Empty, new Size(frame.Width - 1, frame.Height - 1)),
                        new MCvScalar(120, 120, 120));

                    CvInvoke.Rectangle(gray,
                        new Rectangle(Point.Empty, new Size(frame.Width - 1, frame.Height - 1)),
                        new MCvScalar(120, 120, 120));



                    //------------------------------------------------------------------------------------------------------------------------------------------------------------------//

                    //Dibuja las Etiquetas en Imagen original y en Escala de Grices
                    CvInvoke.PutText(frame, "", new Point(20, 20),
                        FontFace.HersheyDuplex, 0.5, new MCvScalar(120, 120, 120));

                    CvInvoke.PutText(gray, "", new Point(20, 20),
                        FontFace.HersheyDuplex, 0.5, new MCvScalar(120, 120, 120));

                    //------------------------------------------------------------------------------------------------------------------------------------------------------------------//

                    //Asigna la Imagen original al pictureBox1
                    pictureBox1.Image = frame.ToBitmap();

                    //------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    // Asigna la imagen en escala de grises al pictureBox2
                    pictureBox2.Image = gray.ToImage<Bgr, byte>().ToBitmap();

                    //------------------------------------------------------------------------------------------------------------------------------------------------------------------//


                    // Asigna la  imagen de contornos al pictureBox3
                    pictureBox3.Image = contoursImage.ToImage<Bgr, byte>().ToBitmap();

                    //------------------------------------------------------------------------------------------------------------------------------------------------------------------//

                    CvInvoke.Imshow(win1, frame);
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        public void Boton_Capturar(object sender, EventArgs e)
        {
            if (capture != null)
            {
                String win1 = " Window (Press any key to close)";
                using (Mat newFrame = new Mat())
                using (UMat newGray = new UMat())
                using (UMat newCannyEdges = new UMat())
                using (UMat newContoursImage = new UMat())
                using (Mat newTriangleRectangleImage = new Mat(newFrame.Size, DepthType.Cv8U, 3))
                {
                    // tomar un nuevo frame de la camara
                    capture.Read(newFrame);
                    CvInvoke.CvtColor(newFrame, newGray, ColorConversion.Bgr2Gray);

                    //Verifique si la imagen está vacía antes de realizar la conversión: 
                    if (!newFrame.IsEmpty && !newGray.IsEmpty && !newContoursImage.IsEmpty)
                    {
                        // Actualiza las Imagenes del PictureBox 
                        pictureBox1.Image = newFrame.ToBitmap();
                        pictureBox2.Image = newGray.ToImage<Bgr, byte>().ToBitmap();
                        pictureBox3.Image = newContoursImage.ToImage<Bgr, byte>().ToBitmap();
                    }
                    else
                    {
                        MessageBox.Show("El newFrame videoCapture no esta inicializado");
                    }



                    CvInvoke.Imshow(win1, newFrame);
                }
            }
            else
            {
                MessageBox.Show("El objeto videoCapture no esta inicializado");
            }
        }
    }
}