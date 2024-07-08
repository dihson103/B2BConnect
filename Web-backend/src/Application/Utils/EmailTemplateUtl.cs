﻿using System.Collections.Specialized;
using Domain.Entities;

namespace Application.Utils;
public class EmailTemplateUtl
{
    public static string GetEventBody(Event @event)
    {

        string html = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html\r\n  dir=\"ltr\"\r\n  xmlns=\"http://www.w3.org/1999/xhtml\"\r\n  xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n  lang=\"vi\"\r\n>\r\n  <head>\r\n    <meta charset=\"UTF-8\" />\r\n    <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\" />\r\n    <meta name=\"x-apple-disable-message-reformatting\" />\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n    <meta content=\"telephone=no\" name=\"format-detection\" />\r\n    <title>New Message</title>\r\n    <!--[if (mso 16)\r\n      ]><style type=\"text/css\">\r\n        a {{\r\n          text-decoration: none;\r\n        }}\r\n      </style><!\r\n    [endif]-->\r\n    <!--[if gte mso 9\r\n      ]><style>\r\n        sup {{\r\n          font-size: 100% !important;\r\n        }}\r\n      </style><!\r\n    [endif]-->\r\n    <!--[if gte mso 9\r\n      ]><xml>\r\n        <o:OfficeDocumentSettings>\r\n          <o:AllowPNG></o:AllowPNG> <o:PixelsPerInch>96</o:PixelsPerInch>\r\n        </o:OfficeDocumentSettings>\r\n      </xml>\r\n    <![endif]-->\r\n    <style type=\"text/css\">\r\n      #outlook a {{\r\n        padding: 0;\r\n      }}\r\n      .es-button {{\r\n        mso-style-priority: 100 !important;\r\n        text-decoration: none !important;\r\n      }}\r\n      a[x-apple-data-detectors] {{\r\n        color: inherit !important;\r\n        text-decoration: none !important;\r\n        font-size: inherit !important;\r\n        font-family: inherit !important;\r\n        font-weight: inherit !important;\r\n        line-height: inherit !important;\r\n      }}\r\n      .es-desk-hidden {{\r\n        display: none;\r\n        float: left;\r\n        overflow: hidden;\r\n        width: 0;\r\n        max-height: 0;\r\n        line-height: 0;\r\n        mso-hide: all;\r\n      }}\r\n      @media only screen and (max-width: 600px) {{\r\n        p,\r\n        ul li,\r\n        ol li,\r\n        a {{\r\n          line-height: 150% !important;\r\n        }}\r\n        h1,\r\n        h2,\r\n        h3,\r\n        h1 a,\r\n        h2 a,\r\n        h3 a {{\r\n          line-height: 120% !important;\r\n        }}\r\n        h1 {{\r\n          font-size: 36px !important;\r\n          text-align: left;\r\n        }}\r\n        h2 {{\r\n          font-size: 26px !important;\r\n          text-align: left;\r\n        }}\r\n        h3 {{\r\n          font-size: 20px !important;\r\n          text-align: left;\r\n        }}\r\n        .es-header-body h1 a,\r\n        .es-content-body h1 a,\r\n        .es-footer-body h1 a {{\r\n          font-size: 36px !important;\r\n          text-align: left;\r\n        }}\r\n        .es-header-body h2 a,\r\n        .es-content-body h2 a,\r\n        .es-footer-body h2 a {{\r\n          font-size: 26px !important;\r\n          text-align: left;\r\n        }}\r\n        .es-header-body h3 a,\r\n        .es-content-body h3 a,\r\n        .es-footer-body h3 a {{\r\n          font-size: 20px !important;\r\n          text-align: left;\r\n        }}\r\n        .es-menu td a {{\r\n          font-size: 12px !important;\r\n        }}\r\n        .es-header-body p,\r\n        .es-header-body ul li,\r\n        .es-header-body ol li,\r\n        .es-header-body a {{\r\n          font-size: 14px !important;\r\n        }}\r\n        .es-content-body p,\r\n        .es-content-body ul li,\r\n        .es-content-body ol li,\r\n        .es-content-body a {{\r\n          font-size: 14px !important;\r\n        }}\r\n        .es-footer-body p,\r\n        .es-footer-body ul li,\r\n        .es-footer-body ol li,\r\n        .es-footer-body a {{\r\n          font-size: 14px !important;\r\n        }}\r\n        .es-infoblock p,\r\n        .es-infoblock ul li,\r\n        .es-infoblock ol li,\r\n        .es-infoblock a {{\r\n          font-size: 12px !important;\r\n        }}\r\n        *[class=\"gmail-fix\"] {{\r\n          display: none !important;\r\n        }}\r\n        .es-m-txt-c,\r\n        .es-m-txt-c h1,\r\n        .es-m-txt-c h2,\r\n        .es-m-txt-c h3 {{\r\n          text-align: center !important;\r\n        }}\r\n        .es-m-txt-r,\r\n        .es-m-txt-r h1,\r\n        .es-m-txt-r h2,\r\n        .es-m-txt-r h3 {{\r\n          text-align: right !important;\r\n        }}\r\n        .es-m-txt-l,\r\n        .es-m-txt-l h1,\r\n        .es-m-txt-l h2,\r\n        .es-m-txt-l h3 {{\r\n          text-align: left !important;\r\n        }}\r\n        .es-m-txt-r img,\r\n        .es-m-txt-c img,\r\n        .es-m-txt-l img {{\r\n          display: inline !important;\r\n        }}\r\n        .es-button-border {{\r\n          display: inline-block !important;\r\n        }}\r\n        a.es-button,\r\n        button.es-button {{\r\n          font-size: 20px !important;\r\n          display: inline-block !important;\r\n        }}\r\n        .es-adaptive table,\r\n        .es-left,\r\n        .es-right {{\r\n          width: 100% !important;\r\n        }}\r\n        .es-content table,\r\n        .es-header table,\r\n        .es-footer table,\r\n        .es-content,\r\n        .es-footer,\r\n        .es-header {{\r\n          width: 100% !important;\r\n          max-width: 600px !important;\r\n        }}\r\n        .es-adapt-td {{\r\n          display: block !important;\r\n          width: 100% !important;\r\n        }}\r\n        .adapt-img {{\r\n          width: 100% !important;\r\n          height: auto !important;\r\n        }}\r\n        .es-m-p0 {{\r\n          padding: 0 !important;\r\n        }}\r\n        .es-m-p0r {{\r\n          padding-right: 0 !important;\r\n        }}\r\n        .es-m-p0l {{\r\n          padding-left: 0 !important;\r\n        }}\r\n        .es-m-p0t {{\r\n          padding-top: 0 !important;\r\n        }}\r\n        .es-m-p0b {{\r\n          padding-bottom: 0 !important;\r\n        }}\r\n        .es-m-p20b {{\r\n          padding-bottom: 20px !important;\r\n        }}\r\n        .es-mobile-hidden,\r\n        .es-hidden {{\r\n          display: none !important;\r\n        }}\r\n        tr.es-desk-hidden,\r\n        td.es-desk-hidden,\r\n        table.es-desk-hidden {{\r\n          width: auto !important;\r\n          overflow: visible !important;\r\n          float: none !important;\r\n          max-height: inherit !important;\r\n          line-height: inherit !important;\r\n        }}\r\n        tr.es-desk-hidden {{\r\n          display: table-row !important;\r\n        }}\r\n        table.es-desk-hidden {{\r\n          display: table !important;\r\n        }}\r\n        td.es-desk-menu-hidden {{\r\n          display: table-cell !important;\r\n        }}\r\n        .es-menu td {{\r\n          width: 1% !important;\r\n        }}\r\n        table.es-table-not-adapt,\r\n        .esd-block-html table {{\r\n          width: auto !important;\r\n        }}\r\n        table.es-social {{\r\n          display: inline-block !important;\r\n        }}\r\n        table.es-social td {{\r\n          display: inline-block !important;\r\n        }}\r\n        .es-m-p5 {{\r\n          padding: 5px !important;\r\n        }}\r\n        .es-m-p5t {{\r\n          padding-top: 5px !important;\r\n        }}\r\n        .es-m-p5b {{\r\n          padding-bottom: 5px !important;\r\n        }}\r\n        .es-m-p5r {{\r\n          padding-right: 5px !important;\r\n        }}\r\n        .es-m-p5l {{\r\n          padding-left: 5px !important;\r\n        }}\r\n        .es-m-p10 {{\r\n          padding: 10px !important;\r\n        }}\r\n        .es-m-p10t {{\r\n          padding-top: 10px !important;\r\n        }}\r\n        .es-m-p10b {{\r\n          padding-bottom: 10px !important;\r\n        }}\r\n        .es-m-p10r {{\r\n          padding-right: 10px !important;\r\n        }}\r\n        .es-m-p10l {{\r\n          padding-left: 10px !important;\r\n        }}\r\n        .es-m-p15 {{\r\n          padding: 15px !important;\r\n        }}\r\n        .es-m-p15t {{\r\n          padding-top: 15px !important;\r\n        }}\r\n        .es-m-p15b {{\r\n          padding-bottom: 15px !important;\r\n        }}\r\n        .es-m-p15r {{\r\n          padding-right: 15px !important;\r\n        }}\r\n        .es-m-p15l {{\r\n          padding-left: 15px !important;\r\n        }}\r\n        .es-m-p20 {{\r\n          padding: 20px !important;\r\n        }}\r\n        .es-m-p20t {{\r\n          padding-top: 20px !important;\r\n        }}\r\n        .es-m-p20r {{\r\n          padding-right: 20px !important;\r\n        }}\r\n        .es-m-p20l {{\r\n          padding-left: 20px !important;\r\n        }}\r\n        .es-m-p25 {{\r\n          padding: 25px !important;\r\n        }}\r\n        .es-m-p25t {{\r\n          padding-top: 25px !important;\r\n        }}\r\n        .es-m-p25b {{\r\n          padding-bottom: 25px !important;\r\n        }}\r\n        .es-m-p25r {{\r\n          padding-right: 25px !important;\r\n        }}\r\n        .es-m-p25l {{\r\n          padding-left: 25px !important;\r\n        }}\r\n        .es-m-p30 {{\r\n          padding: 30px !important;\r\n        }}\r\n        .es-m-p30t {{\r\n          padding-top: 30px !important;\r\n        }}\r\n        .es-m-p30b {{\r\n          padding-bottom: 30px !important;\r\n        }}\r\n        .es-m-p30r {{\r\n          padding-right: 30px !important;\r\n        }}\r\n        .es-m-p30l {{\r\n          padding-left: 30px !important;\r\n        }}\r\n        .es-m-p35 {{\r\n          padding: 35px !important;\r\n        }}\r\n        .es-m-p35t {{\r\n          padding-top: 35px !important;\r\n        }}\r\n        .es-m-p35b {{\r\n          padding-bottom: 35px !important;\r\n        }}\r\n        .es-m-p35r {{\r\n          padding-right: 35px !important;\r\n        }}\r\n        .es-m-p35l {{\r\n          padding-left: 35px !important;\r\n        }}\r\n        .es-m-p40 {{\r\n          padding: 40px !important;\r\n        }}\r\n        .es-m-p40t {{\r\n          padding-top: 40px !important;\r\n        }}\r\n        .es-m-p40b {{\r\n          padding-bottom: 40px !important;\r\n        }}\r\n        .es-m-p40r {{\r\n          padding-right: 40px !important;\r\n        }}\r\n        .es-m-p40l {{\r\n          padding-left: 40px !important;\r\n        }}\r\n        .es-desk-hidden {{\r\n          display: table-row !important;\r\n          width: auto !important;\r\n          overflow: visible !important;\r\n          max-height: inherit !important;\r\n        }}\r\n      }}\r\n      @media screen and (max-width: 384px) {{\r\n        .mail-message-content {{\r\n          width: 414px !important;\r\n        }}\r\n      }}\r\n    </style>\r\n  </head>\r\n  <body\r\n    bis_status=\"ok\"\r\n    style=\"\r\n      width: 100%;\r\n      font-family: arial, 'helvetica neue', helvetica, sans-serif;\r\n      -webkit-text-size-adjust: 100%;\r\n      -ms-text-size-adjust: 100%;\r\n      padding: 0;\r\n      margin: 0;\r\n    \"\r\n  >\r\n    <div\r\n      dir=\"ltr\"\r\n      class=\"es-wrapper-color\"\r\n      lang=\"vi\"\r\n      style=\"background-color: #fafafa\"\r\n    >\r\n      <!--[if gte mso 9\r\n        ]><v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\r\n          <v:fill type=\"tile\" color=\"#fafafa\"></v:fill> </v:background\r\n      ><![endif]-->\r\n      <table\r\n        class=\"es-wrapper\"\r\n        width=\"100%\"\r\n        cellspacing=\"0\"\r\n        cellpadding=\"0\"\r\n        role=\"none\"\r\n        style=\"\r\n          mso-table-lspace: 0pt;\r\n          mso-table-rspace: 0pt;\r\n          border-collapse: collapse;\r\n          border-spacing: 0px;\r\n          padding: 0;\r\n          margin: 0;\r\n          width: 100%;\r\n          height: 100%;\r\n          background-repeat: repeat;\r\n          background-position: center top;\r\n          background-color: #fafafa;\r\n        \"\r\n      >\r\n        <tr>\r\n          <td valign=\"top\" style=\"padding: 0; margin: 0\">\r\n            <table\r\n              cellpadding=\"0\"\r\n              cellspacing=\"0\"\r\n              class=\"es-header\"\r\n              align=\"center\"\r\n              role=\"none\"\r\n              style=\"\r\n                mso-table-lspace: 0pt;\r\n                mso-table-rspace: 0pt;\r\n                border-collapse: collapse;\r\n                border-spacing: 0px;\r\n                table-layout: fixed !important;\r\n                width: 100%;\r\n                background-color: transparent;\r\n                background-repeat: repeat;\r\n                background-position: center top;\r\n              \"\r\n            >\r\n              <tr>\r\n                <td align=\"center\" style=\"padding: 0; margin: 0\">\r\n                  <table\r\n                    bgcolor=\"#ffffff\"\r\n                    class=\"es-header-body\"\r\n                    align=\"center\"\r\n                    cellpadding=\"0\"\r\n                    cellspacing=\"0\"\r\n                    role=\"none\"\r\n                    style=\"\r\n                      mso-table-lspace: 0pt;\r\n                      mso-table-rspace: 0pt;\r\n                      border-collapse: collapse;\r\n                      border-spacing: 0px;\r\n                      background-color: transparent;\r\n                      width: 600px;\r\n                    \"\r\n                  >\r\n                    <tr>\r\n                      <td\r\n                        align=\"left\"\r\n                        style=\"\r\n                          margin: 0;\r\n                          padding-top: 10px;\r\n                          padding-bottom: 10px;\r\n                          padding-left: 20px;\r\n                          padding-right: 20px;\r\n                        \"\r\n                      >\r\n                        <table\r\n                          cellpadding=\"0\"\r\n                          cellspacing=\"0\"\r\n                          width=\"100%\"\r\n                          role=\"none\"\r\n                          style=\"\r\n                            mso-table-lspace: 0pt;\r\n                            mso-table-rspace: 0pt;\r\n                            border-collapse: collapse;\r\n                            border-spacing: 0px;\r\n                          \"\r\n                        >\r\n                          <tr>\r\n                            <td\r\n                              class=\"es-m-p0r\"\r\n                              valign=\"top\"\r\n                              align=\"center\"\r\n                              style=\"padding: 0; margin: 0; width: 560px\"\r\n                            >\r\n                              <table\r\n                                cellpadding=\"0\"\r\n                                cellspacing=\"0\"\r\n                                width=\"100%\"\r\n                                role=\"presentation\"\r\n                                style=\"\r\n                                  mso-table-lspace: 0pt;\r\n                                  mso-table-rspace: 0pt;\r\n                                  border-collapse: collapse;\r\n                                  border-spacing: 0px;\r\n                                \"\r\n                              >\r\n                                <tr>\r\n                                  <td\r\n                                    align=\"center\"\r\n                                    style=\"\r\n                                      padding: 0;\r\n                                      margin: 0;\r\n                                      padding-bottom: 20px;\r\n                                      font-size: 0px;\r\n                                    \"\r\n                                  >\r\n                                    <img\r\n                                      src=\"{@event.Image}\"\r\n                                      alt=\"Logo\"\r\n                                      style=\"\r\n                                        display: block;\r\n                                        border: 0;\r\n                                        outline: none;\r\n                                        text-decoration: none;\r\n                                        -ms-interpolation-mode: bicubic;\r\n                                        font-size: 12px;\r\n                                      \"\r\n                                      width=\"200\"\r\n                                      title=\"Logo\"\r\n                                    />\r\n                                  </td>\r\n                                </tr>\r\n                              </table>\r\n                            </td>\r\n                          </tr>\r\n                        </table>\r\n                      </td>\r\n                    </tr>\r\n                  </table>\r\n                </td>\r\n              </tr>\r\n            </table>\r\n            <table\r\n              cellpadding=\"0\"\r\n              cellspacing=\"0\"\r\n              class=\"es-content\"\r\n              align=\"center\"\r\n              role=\"none\"\r\n              style=\"\r\n                mso-table-lspace: 0pt;\r\n                mso-table-rspace: 0pt;\r\n                border-collapse: collapse;\r\n                border-spacing: 0px;\r\n                table-layout: fixed !important;\r\n                width: 100%;\r\n              \"\r\n            >\r\n              <tr>\r\n                <td align=\"center\" style=\"padding: 0; margin: 0\">\r\n                  <table\r\n                    bgcolor=\"#ffffff\"\r\n                    class=\"es-content-body\"\r\n                    align=\"center\"\r\n                    cellpadding=\"0\"\r\n                    cellspacing=\"0\"\r\n                    style=\"\r\n                      mso-table-lspace: 0pt;\r\n                      mso-table-rspace: 0pt;\r\n                      border-collapse: collapse;\r\n                      border-spacing: 0px;\r\n                      background-color: #ffffff;\r\n                      width: 600px;\r\n                    \"\r\n                    role=\"none\"\r\n                  >\r\n                    <tr>\r\n                      <td\r\n                        align=\"left\"\r\n                        style=\"\r\n                          padding: 0;\r\n                          margin: 0;\r\n                          padding-top: 20px;\r\n                          padding-left: 20px;\r\n                          padding-right: 20px;\r\n                        \"\r\n                      >\r\n                        <table\r\n                          cellpadding=\"0\"\r\n                          cellspacing=\"0\"\r\n                          width=\"100%\"\r\n                          role=\"none\"\r\n                          style=\"\r\n                            mso-table-lspace: 0pt;\r\n                            mso-table-rspace: 0pt;\r\n                            border-collapse: collapse;\r\n                            border-spacing: 0px;\r\n                          \"\r\n                        >\r\n                          <tr>\r\n                            <td\r\n                              align=\"center\"\r\n                              valign=\"top\"\r\n                              style=\"padding: 0; margin: 0; width: 560px\"\r\n                            >\r\n                              <table\r\n                                cellpadding=\"0\"\r\n                                cellspacing=\"0\"\r\n                                width=\"100%\"\r\n                                role=\"presentation\"\r\n                                style=\"\r\n                                  mso-table-lspace: 0pt;\r\n                                  mso-table-rspace: 0pt;\r\n                                  border-collapse: collapse;\r\n                                  border-spacing: 0px;\r\n                                \"\r\n                              >\r\n                                <tr>\r\n                                  <td\r\n                                    align=\"center\"\r\n                                    class=\"es-m-txt-c\"\r\n                                    style=\"\r\n                                      padding: 0;\r\n                                      margin: 0;\r\n                                      padding-top: 5px;\r\n                                      padding-bottom: 5px;\r\n                                    \"\r\n                                  >\r\n                                    <h1\r\n                                      style=\"\r\n                                        margin: 0;\r\n                                        line-height: 55px;\r\n                                        mso-line-height-rule: exactly;\r\n                                        font-family: arial, 'helvetica neue',\r\n                                          helvetica, sans-serif;\r\n                                        font-size: 46px;\r\n                                        font-style: normal;\r\n                                        font-weight: bold;\r\n                                        color: #333333;\r\n                                      \"\r\n                                    >\r\n                                      Sự kiện mới\r\n                                    </h1>\r\n                                  </td>\r\n                                </tr>\r\n                                <tr>\r\n                                  <td\r\n                                    align=\"center\"\r\n                                    style=\"\r\n                                      padding: 0;\r\n                                      margin: 0;\r\n                                      padding-top: 10px;\r\n                                      padding-bottom: 10px;\r\n                                      font-size: 0px;\r\n                                    \"\r\n                                  >\r\n                                    <img\r\n                                      class=\"adapt-img\"\r\n                                      src=\"https://fncnlfo.stripocdn.email/content/guids/CABINET_dfd027a58d4f99b4ad45db289ee5f2ac/images/32131618213418547.jpg\"\r\n                                      alt\r\n                                      style=\"\r\n                                        display: block;\r\n                                        border: 0;\r\n                                        outline: none;\r\n                                        text-decoration: none;\r\n                                        -ms-interpolation-mode: bicubic;\r\n                                      \"\r\n                                      width=\"350\"\r\n                                    />\r\n                                  </td>\r\n                                </tr>\r\n                                <tr>\r\n                                  <td\r\n                                    align=\"center\"\r\n                                    class=\"es-m-txt-c\"\r\n                                    style=\"\r\n                                      padding: 0;\r\n                                      margin: 0;\r\n                                      padding-top: 10px;\r\n                                    \"\r\n                                  >\r\n                                    <h2\r\n                                      style=\"\r\n                                        margin: 0;\r\n                                        line-height: 31px;\r\n                                        mso-line-height-rule: exactly;\r\n                                        font-family: arial, 'helvetica neue',\r\n                                          helvetica, sans-serif;\r\n                                        font-size: 26px;\r\n                                        font-style: normal;\r\n                                        font-weight: bold;\r\n                                        color: #333333;\r\n                                      \"\r\n                                    >\r\n   {@event.Name}    </h2>\r\n                                  </td>\r\n                                </tr>\r\n                                <tr>\r\n                                  <td\r\n                                    align=\"center\"\r\n                                    style=\"\r\n                                      padding: 0;\r\n                                      margin: 0;\r\n                                      padding-top: 5px;\r\n                                      padding-bottom: 5px;\r\n                                    \"\r\n                                  >\r\n                                    <p\r\n                                      style=\"\r\n                                        margin: 0;\r\n                                        -webkit-text-size-adjust: none;\r\n                                        -ms-text-size-adjust: none;\r\n                                        mso-line-height-rule: exactly;\r\n                                        font-family: arial, 'helvetica neue',\r\n                                          helvetica, sans-serif;\r\n                                        line-height: 21px;\r\n                                        color: #333333;\r\n                                        font-size: 14px;\r\n                                      \"\r\n                                    >\r\n                                      Thời gian: {@event.StartAt} - {@event.EndAt}\r\n                                      08:00\r\n                                    </p>\r\n                                  </td>\r\n                                </tr>\r\n                                <tr>\r\n                                  <td\r\n                                    align=\"center\"\r\n                                    style=\"\r\n                                      padding: 0;\r\n                                      margin: 0;\r\n                                      padding-top: 5px;\r\n                                      padding-bottom: 5px;\r\n                                    \"\r\n                                  >\r\n                                    <p\r\n                                      style=\"\r\n                                        margin: 0;\r\n                                        -webkit-text-size-adjust: none;\r\n                                        -ms-text-size-adjust: none;\r\n                                        mso-line-height-rule: exactly;\r\n                                        font-family: arial, 'helvetica neue',\r\n                                          helvetica, sans-serif;\r\n                                        line-height: 21px;\r\n                                        color: #333333;\r\n                                        font-size: 14px;\r\n                                      \"\r\n                                    >\r\n                                      Địa điểm: {@event.Location}\r\n                                    </p>\r\n                                  </td>\r\n                                </tr>\r\n                              </table>\r\n                            </td>\r\n                          </tr>\r\n                        </table>\r\n                      </td>\r\n                    </tr>\r\n                    <tr>\r\n                      <td\r\n                        align=\"left\"\r\n                        style=\"\r\n                          padding: 0;\r\n                          margin: 0;\r\n                          padding-bottom: 10px;\r\n                          padding-left: 20px;\r\n                          padding-right: 20px;\r\n                        \"\r\n                      >\r\n                        <table\r\n                          cellpadding=\"0\"\r\n                          cellspacing=\"0\"\r\n                          width=\"100%\"\r\n                          role=\"none\"\r\n                          style=\"\r\n                            mso-table-lspace: 0pt;\r\n                            mso-table-rspace: 0pt;\r\n                            border-collapse: collapse;\r\n                            border-spacing: 0px;\r\n                          \"\r\n                        >\r\n                          <tr>\r\n                            <td\r\n                              align=\"center\"\r\n                              valign=\"top\"\r\n                              style=\"padding: 0; margin: 0; width: 560px\"\r\n                            >\r\n                              <table\r\n                                cellpadding=\"0\"\r\n                                cellspacing=\"0\"\r\n                                width=\"100%\"\r\n                                role=\"presentation\"\r\n                                style=\"\r\n                                  mso-table-lspace: 0pt;\r\n                                  mso-table-rspace: 0pt;\r\n                                  border-collapse: collapse;\r\n                                  border-spacing: 0px;\r\n                                \"\r\n                              >\r\n                                <tr>\r\n                                  <td\r\n                                    align=\"center\"\r\n                                    style=\"\r\n                                      padding: 0;\r\n                                      margin: 0;\r\n                                      padding-bottom: 10px;\r\n                                      padding-top: 15px;\r\n                                    \"\r\n                                  >\r\n                                    <span\r\n                                      class=\"es-button-border\"\r\n                                      style=\"\r\n                                        border-style: solid;\r\n                                        border-color: #5c68e2;\r\n                                        background: #5c68e2;\r\n                                        border-width: 2px;\r\n                                        display: inline-block;\r\n                                        border-radius: 5px;\r\n                                        width: auto;\r\n                                      \"\r\n                                      ><a\r\n                                        href=\"https://www.facebook.com/\"\r\n                                        class=\"es-button es-button-1621341519989\"\r\n                                        target=\"_blank\"\r\n                                        style=\"\r\n                                          mso-style-priority: 100 !important;\r\n                                          text-decoration: none;\r\n                                          -webkit-text-size-adjust: none;\r\n                                          -ms-text-size-adjust: none;\r\n                                          mso-line-height-rule: exactly;\r\n                                          color: #ffffff;\r\n                                          font-size: 20px;\r\n                                          padding: 10px 30px;\r\n                                          display: inline-block;\r\n                                          background: #5c68e2;\r\n                                          border-radius: 5px;\r\n                                          font-family: arial, 'helvetica neue',\r\n                                            helvetica, sans-serif;\r\n                                          font-weight: normal;\r\n                                          font-style: normal;\r\n                                          line-height: 24px;\r\n                                          width: auto;\r\n                                          text-align: center;\r\n                                          mso-padding-alt: 0;\r\n                                          mso-border-alt: 10px solid #5c68e2;\r\n                                        \"\r\n                                        >Xem chi tiết</a\r\n                                      >\r\n                                    </span>\r\n                                  </td>\r\n                                </tr>\r\n                              </table>\r\n                            </td>\r\n                          </tr>\r\n                        </table>\r\n                      </td>\r\n                    </tr>\r\n                  </table>\r\n                </td>\r\n              </tr>\r\n            </table>\r\n          </td>\r\n        </tr>\r\n      </table>\r\n    </div>\r\n  </body>\r\n</html>\r\n";
        return html;
    }
}
