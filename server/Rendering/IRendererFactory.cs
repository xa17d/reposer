using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reposer.Rendering
{
    public interface IRendererFactory
    {
        IRenderer Create();
    }
}
