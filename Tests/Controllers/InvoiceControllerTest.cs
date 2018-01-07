using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ESolutions.Wtmt.Controllers;
using System.Web.Mvc;

namespace ESolutions.Wtmt.Tests.Controllers
{
    [TestClass]
    public class InvoicesControllerTest
    {
        [TestMethod]
        public void MergeFiles()
        {
            // Arrange
            InvoicesController controller = new InvoicesController();

            // Act

            List<FileInfo> sourceFiles = new List<FileInfo> ();
            sourceFiles.Add(new FileInfo("c:\\pdf\\a.pdf"));
            sourceFiles.Add(new FileInfo("c:\\pdf\\b.pdf"));            
            ActionResult result = controller.MergeFiles(sourceFiles);            

            //Assert
            Assert.Fail();
        }        

    }
}
