//  <copyright file="HexagonShape.cs" company="NIP">
//  Copyright © 2018. All rights reserved.
//  </copyright>
//  <author>Vasyl Salabay</author>
//  <date>09/15/2018 05:09:42 PM </date>
//  <summary>Class representing a hexagon</summary>

namespace HexagonLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Class represent hexagon
    /// </summary>
    public class HexagonShape : INotifyPropertyChanged
    {
        /// <summary>
        /// IsChoose field
        /// </summary>
        private bool isChoosen;

        /// <summary>
        /// Color field
        /// </summary>
        private Color color = Color.FromArgb(0, 255, 255, 255);

        /// <summary>
        /// Margin field
        /// </summary>
        private Point margin;

        /// <summary>
        /// Point list
        /// </summary>
        public List<Point> PointList
        {
            get;
            set;
        }

        /// <summary>
        /// Name property
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// IsChosen property
        /// </summary>
        public bool IsChoosen
        {
            get
            {
                return isChoosen;
            }

            set
            {
                if (value != this.isChoosen)
                {
                    this.isChoosen = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// PropertyChangedEventHandler event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// NotifyPropertyChanges method
        /// </summary>
        /// <param name="propertyName">Property name</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public HexagonShape()
        {
        }

        public HexagonShape(string name, List<Point> pointList, bool isChose = false)
        {
            Name = name;
            IsChoosen = isChoosen;
            PointList = new List<Point>();
            foreach (var item in pointList)
            {
                PointList.Add(item);
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                if (value != this.color)
                {
                    this.color = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Point Margin
        {
            get
            {
                return margin;
            }

            set
            {
                if (value != this.margin)
                {
                    this.margin = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
