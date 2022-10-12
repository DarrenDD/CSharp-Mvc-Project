using SpringOceanTechnologiesIMS.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SpringOceanTechnologiesIMS.Comparers
{
    public class CompareProgrammes : IEqualityComparer<Programme>
    {
        public bool Equals(Programme x, Programme y)
        {
            if (y == null) return false;

            if (x.Id == y.Id)
                return true;

            return false;

        }

        public int GetHashCode([DisallowNull] Programme obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
