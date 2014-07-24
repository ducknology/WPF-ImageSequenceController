using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace DcUtil.Animation
{
	public class ImageSequenceController
	{
		private WriteableBitmap _writeArea	= null;
		private Image _target				= new Image();
		private byte[][] _frames			= null;
		private int _imgStrides				= 0;
		private int _singleBufferSize		= 0;
		private Int32Rect _imgRect;

		private double _durationInSec		= 0;
		private double _timeIndex			= 0;
		private int _frameIndex				= 0;
		private int _totalFrame				= 0;
		
		public int FramePerSec				= 30;
		public bool Repeat					= true;

		public ImageSequenceController( Image imageTarget, string[] fileList, int fps = 30 )
		{
			_target = imageTarget;
			_frames = new byte[fileList.Length][];

			for( int i = 0; i < fileList.Length; ++i )
			{
				string filePath = fileList[i];
				Uri uri = new Uri( filePath, UriKind.Relative );
				BitmapFrame frame = BitmapFrame.Create( uri );
				if( _imgStrides == 0 )
				{
					_imgStrides = frame.PixelWidth * frame.Format.BitsPerPixel / 8;
					_singleBufferSize = _imgStrides * frame.PixelHeight;
				}
				
				byte[] newBuffer = new byte[_singleBufferSize];
				frame.CopyPixels( newBuffer, _imgStrides, 0 );

				if( _imgRect.Width == 0 )
				{
					_imgRect.Width	= frame.PixelWidth;
					_imgRect.Height	= frame.PixelHeight;
				}

				if( _writeArea == null )
				{
					_writeArea = new WriteableBitmap( frame );
				}
			}

			_target.Source = _writeArea;
			FramePerSec = fps;
			_totalFrame = _frames.Length;
			_durationInSec = _frames.Length / (double)FramePerSec;
		}

		public void Update( double interval )
		{
			_timeIndex += interval;

		}

		private void UpdateFrameIndex();
		{
			_frameIndex = 
		}
	}
}
