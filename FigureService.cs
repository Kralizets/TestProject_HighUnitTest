﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestProject
{
    public class FigureService : IFigureService
    {
        IEnumerable<BaseFigure> _baseFigures { get; set; }

        public FigureService(IEnumerable<BaseFigure> figures)
        {
            if (figures == null)
            {
                _baseFigures = new BaseFigure[0];
                return;
            }                

            _baseFigures = figures;
        }

        public double GetSumAreas() => _baseFigures.Any() ? 0d : _baseFigures.Sum(x => x.Area()); // В IEnumerable Any() будет быстрее, если последовательность не пустая

        public async Task<double> GetSumAreasAsync(CancellationToken cancellationToken)
        {
            double sum = 0d;
            await Task.Run(() => sum = _baseFigures.Sum(x => x.Area()), cancellationToken);

            return sum;
        }
    }

    public interface IFigureService
    {
        public double GetSumAreas();

        public Task<double> GetSumAreasAsync(CancellationToken cancellationToken);
    }
}
