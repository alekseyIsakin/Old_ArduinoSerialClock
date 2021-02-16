﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;

using ArdClock.src;
using ArdClock.src.ArdPage;
using ArdClock.src.HelpingClass;
using ArdClock.src.UIGenerate;
using ArdClock.src.ArdPage.PageElements;

namespace ArdClock.src.XMLLoader
{
    static class Writer
    {
        static public void WritePageListToXML(List<APage> pageList, string fileName)
        {
            XmlDocument xdd = new XmlDocument();
            var xmlDeclaration = xdd.CreateXmlDeclaration("1.0", "UTF-8", null);
            var root = xdd.CreateElement(
                (XMLDefines.XMLTag.Pages).ToString());

            xdd.AppendChild(xmlDeclaration);
            xdd.AppendChild(root);

            foreach (var page in pageList) 
            {
                // Создаём нод для описания страницы
                var xmlPage = xdd.CreateElement(
                    (XMLDefines.XMLTag.Page).ToString());

                // Аттрибут для описания имени и ID
                var attrName =
                    xdd.CreateAttribute(XMLDefines.XMLPageAttr.Name.ToString());
                var attrID =
                    xdd.CreateAttribute(XMLDefines.XMLPageAttr.ID.ToString());

                attrName.Value = page.Name;
                attrID.Value = page.ID.ToString();

                xmlPage.Attributes.Append(attrName);
                xmlPage.Attributes.Append(attrID);

                foreach (var pageEl in page.Elements) 
                {
                    switch (pageEl.GetTypeEl()) 
                    {
                        case TPageEl.String:
                            xmlPage.AppendChild(
                                XmlElFromPageString((PageString)pageEl, xdd));
                            break;
                        case TPageEl.Time:
                            xmlPage.AppendChild(
                                XmlElFromPageTime((PageTime)pageEl, xdd));
                            break;

                    }
                }

                root.AppendChild(xmlPage);
            }

            try
            { xdd.Save(fileName); }
            catch
            { }
        }

        static private XmlElement XmlElFromPageString(PageString ps, XmlDocument xdd)
        {
            // Описание элемента
            var ndPageEl = xdd.CreateElement(
                (XMLDefines.XMLTag.PageEl).ToString());

            var attrTypeEl = xdd.CreateAttribute(
                XMLDefines.XMLStringAttr.TypeEl.ToString());

            attrTypeEl.Value = ((int)ps.GetTypeEl()).ToString();

            ndPageEl.Attributes.Append(attrTypeEl);

            // Позиция
            var ndPos = xdd.CreateElement(
                XMLDefines.XMLStringTag.Position.ToString());

            var attrPosX = xdd.CreateAttribute(
                XMLDefines.XMLStringAttr.PosX.ToString());
            var attrPosY = xdd.CreateAttribute(
                XMLDefines.XMLStringAttr.PosY.ToString());

            attrPosX.Value = ps.X.ToString();
            attrPosY.Value = ps.Y.ToString();

            ndPos.Attributes.Append(attrPosX);
            ndPos.Attributes.Append(attrPosY);

            // Цвет
            var ndClr = xdd.CreateElement(
                XMLDefines.XMLStringTag.Color.ToString());

            var attrClr = xdd.CreateAttribute(
                XMLDefines.XMLStringAttr.ColorValue.ToString());

            attrClr.Value = ps.TextColor.ToHex();

            ndClr.Attributes.Append(attrClr);

            // Размер
            var ndSz = xdd.CreateElement(
                XMLDefines.XMLStringTag.Size.ToString());

            var attrSz = xdd.CreateAttribute(
                XMLDefines.XMLStringAttr.SizeValue.ToString());

            attrSz.Value = ps.Size.ToString();

            ndSz.Attributes.Append(attrSz);

            // Текст
            var ndDt = xdd.CreateElement(
                XMLDefines.XMLStringTag.Data.ToString());

            var attrDt = xdd.CreateAttribute(
                XMLDefines.XMLStringAttr.Data.ToString());

            attrDt.Value = ps.Data;

            ndDt.Attributes.Append(attrDt);


            //
            ndPageEl.AppendChild(ndPos);
            ndPageEl.AppendChild(ndClr);
            ndPageEl.AppendChild(ndSz);
            ndPageEl.AppendChild(ndDt);

            return ndPageEl;
        }


        static private XmlElement XmlElFromPageTime(PageTime pt, XmlDocument xdd)
        {
            // Описание элемента
            var ndPageEl = xdd.CreateElement(
                (XMLDefines.XMLTag.PageEl).ToString());

            var attrTypeEl = xdd.CreateAttribute(
                XMLDefines.XMLTimeAttr.TypeEl.ToString());

            attrTypeEl.Value = ((int)pt.GetTypeEl()).ToString();

            ndPageEl.Attributes.Append(attrTypeEl);

            // Позиция
            var ndPos = xdd.CreateElement(
                XMLDefines.XMLTimeTag.Position.ToString());

            var attrPosX = xdd.CreateAttribute(
                XMLDefines.XMLTimeAttr.PosX.ToString());
            var attrPosY = xdd.CreateAttribute(
                XMLDefines.XMLTimeAttr.PosY.ToString());

            attrPosX.Value = pt.X.ToString();
            attrPosY.Value = pt.Y.ToString();

            ndPos.Attributes.Append(attrPosX);
            ndPos.Attributes.Append(attrPosY);

            // Цвет
            var ndClr = xdd.CreateElement(
                XMLDefines.XMLTimeTag.Color.ToString());

            var attrClr = xdd.CreateAttribute(
                XMLDefines.XMLTimeAttr.ColorValue.ToString());

            attrClr.Value = pt.TextColor.ToHex();

            ndClr.Attributes.Append(attrClr);

            // Размер
            var ndSz = xdd.CreateElement(
                XMLDefines.XMLTimeTag.Size.ToString());

            var attrSz = xdd.CreateAttribute(
                XMLDefines.XMLTimeAttr.SizeValue.ToString());

            attrSz.Value = pt.Size.ToString();

            ndSz.Attributes.Append(attrSz);

            // Данные
            var ndDt = xdd.CreateElement(
                XMLDefines.XMLTimeTag.Data.ToString());

            var attrSec = xdd.CreateAttribute(
                XMLDefines.XMLTimeAttr.DataSec.ToString());
            var attrMin = xdd.CreateAttribute(
                XMLDefines.XMLTimeAttr.DataMin.ToString());
            var attrHour = xdd.CreateAttribute(
                XMLDefines.XMLTimeAttr.DataHour.ToString());

            attrSec.Value = pt.Second ? "1" : "0";
            attrMin.Value = pt.Minut ? "1" : "0";
            attrHour.Value = pt.Hour ? "1" : "0";

            ndDt.Attributes.Append(attrSec);
            ndDt.Attributes.Append(attrMin);
            ndDt.Attributes.Append(attrHour);


            //
            ndPageEl.AppendChild(ndPos);
            ndPageEl.AppendChild(ndClr);
            ndPageEl.AppendChild(ndSz);
            ndPageEl.AppendChild(ndDt);

            return ndPageEl;
        }
    }
}
