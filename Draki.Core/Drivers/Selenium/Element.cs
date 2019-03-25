﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Draki.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Draki
{
    public class Element : IElement
    {
        private string selector = null;

        public Element(IWebElement webElement, string selector)
        {
            this.WebElement = webElement;
            this.selector = selector;
            this.tagName = this.WebElement.TagName;
        }

        private string tagName = null;
        public string TagName
        {
            get
            {
                return this.tagName;
            }
        }

        public string Value
        {
            get
            {
                if (this.TagName == "input" || this.TagName == "textarea")
                {
                    return this.Attributes.Get("value");
                }
                else if (this.IsSelect)
                {
                    return string.Join(",", this.SelectedOptionValues);
                }
                else
                {
                    return this.Text;
                }
            }
        }

        public string Text
        {
            get
            {
                if (this.TagName == "input" || this.TagName == "textarea")
                {
                    return this.Value;
                }
                else
                {
                    return this.WebElement.Text;
                }
            }
        }

        public string Selector
        {
            get
            {
                return this.selector;
            }
        }

        public IEnumerable<string> SelectedOptionValues
        {
            get
            {
                if (this.IsSelect)
                {
                    SelectElement selectElement = new SelectElement(this.WebElement);
                    return selectElement.AllSelectedOptions.Select(x => x.GetAttribute("value"));
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<string> SelectedOptionTextCollection
        {
            get
            {
                if (this.IsSelect)
                {
                    SelectElement selectElement = new SelectElement(this.WebElement);
                    return selectElement.AllSelectedOptions.Select(x => x.Text);
                }
                else
                {
                    return null;
                }
            }
        }

        public bool IsText
        {
            get
            {
                bool isText = false;
                switch (this.TagName)
                {
                    case "input":
                        var inputType = this.Attributes.Get("type").ToLower();
                        switch (inputType)
                        {
                            case "file":
                            case "text":
                            case "email":
                            case "search":
                            case "url":
                            case "tel":
                            case "number":
                            case "password":
                            case "hidden":
                                isText = true;
                                break;
                        }

                        break;
                    case "textarea":
                        isText = true;
                        break;
                }

                return isText;
            }
        }

        public bool IsSelect
        {
            get
            {
                return this.WebElement.TagName.ToLower() == "select";
            }
        }

        public bool IsMultipleSelect
        {
            get
            {
                if (this.IsSelect)
                {
                    SelectElement selectElement = new SelectElement(this.WebElement);
                    return selectElement.IsMultiple;
                }
                else
                {
                    return false;
                }
            }
        }

        private IElementAttributeSelector attributes = null;
        public IElementAttributeSelector Attributes
        {
            get
            {
                if (attributes == null)
                {
                    attributes = new ElementAttributeSelector(this.WebElement);
                }

                return attributes;
            }
        }

        public IWebElement WebElement { get; set; }

        public int Height
        {
            get
            {
                return this.WebElement.Size.Height;
            }
        }

        public int Width
        {
            get
            {
                return this.WebElement.Size.Width;
            }
        }

        public int PosX
        {
            get
            {
                return this.WebElement.Location.X;
            }
        }

        public int PosY
        {
            get
            {
                return this.WebElement.Location.Y;
            }
        }
    }

    public class ElementAttributeSelector : IElementAttributeSelector
    {
        private readonly IWebElement webElement = null;

        public ElementAttributeSelector(IWebElement webElement)
        {
            this.webElement = webElement;
        }

        public string Get(string name)
        {
            var attributeValue = webElement.GetAttribute(name);
            return attributeValue;
        }
    }
}
