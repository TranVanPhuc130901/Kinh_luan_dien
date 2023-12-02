/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/
CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    CKEDITOR.config.toolbar_Max =
        [
            ["Source", "-", "Save", "NewPage", "Preview", "-", "Templates"],
            ["Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-", "Print", "SpellChecker", "Scayt"],
            ["Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat"],
            ["Form", "Checkbox", "Radio", "TextField", "Textarea", "Select", "Button", "ImageButton", "HiddenField"],
            ["Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript"],
            ["NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv"],
            ["JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock"],
            ["BidiLtr", "BidiRtl"],
            ["Link", "Unlink", "Anchor"],
            ["Image", "Flash", "Table", "HorizontalRule", "Smiley", "SpecialChar", "PageBreak", "Iframe"],
            ["Styles", "Format", "Font", "FontSize"],
            ["TextColor", "BGColor"],
            ["Maximize", "ShowBlocks", "-", "About"]
        ],
        CKEDITOR.config.toolbar_Standard =
        [
            ["Source", "-", "Preview", "-", "Templates"],
            ["Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-", "Print", "SpellChecker", "Scayt"],
            ["Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat"],
            ["Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript"],
            ["NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv"],
            ["JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock"],
            ["BidiLtr", "BidiRtl"],
            ["Link", "Unlink", "Anchor"],
            ["Image", "Youtube", "MediaEmbed", "Iframe", "Flash", "-", "Table", "HorizontalRule", "PageBreak", "Smiley", "SpecialChar"],
            ["Styles", "Format", "Font", "FontSize"],
            ["TextColor", "BGColor"],
            ["Maximize", "ShowBlocks"]
        ],
        CKEDITOR.config.toolbar_Basic =
        [
            ["Source"],
            ["JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock"],
            ["Bold", "Italic", "Underline", "Strike", "-", "TextColor", "BGColor"],
            ["NumberedList", "BulletedList"],
            ["Table","-" ,"Link", "Unlink"],
            ["Font", "FontSize"], ["RemoveFormat"]
        ],
        CKEDITOR.config.toolbar_Min =
        [
            ["Bold", "Italic", "Underline", "Strike", "TextColor"],
            ["NumberedList", "BulletedList", "Outdent", "Indent"], ["RemoveFormat"]
        ],

        CKEDITOR.config.toolbar_StandardComment =
        [
            ["Maximize"],
            ["JustifyLeft", "JustifyCenter", "JustifyRight"],
            ["Bold", "Italic", "Underline", "Strike", "-", "TextColor", "BGColor"],
            ["FontSize"],
            ["NumberedList", "BulletedList"],
            ["Image", "Youtube", "-", "Table", "SpecialChar", "-", "Link", "Unlink"],
            ["RemoveFormat", "Source"]
        ],


        config.extraPlugins = "filebrowser,popup,uploadimage,uploadwidget,widget,clipboard,dialog,dialogui,lineutils,filetools,notificationaggregator,notification,toolbar,button,image2,youtube,mediaembed,pastefromword,codesnippet,smiley,blockquote,wordcount";

    //http://docs.ckeditor.com/#!/guide/dev_codesnippet

    config.skin = "moono";
    config.language = "vi";
    //config.enterMode = CKEDITOR.ENTER_BR;
    //config.shiftEnterMode = CKEDITOR.ENTER_P;
    config.height = "420";
    config.toolbar = "Standard";


    //Cấu hình Uploading Dropped and Pasted Images
    // Upload images to a CKFinder connector (note that the response type is set to JSON).
    config.uploadUrl = "ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images&responseType=json";

    config.filebrowserBrowseUrl = "ckeditor/ckfinder/ckfinder.aspx?type=Files";
    config.filebrowserImageBrowseUrl = "ckeditor/ckfinder/ckfinder.aspx?type=Images";
    config.filebrowserFlashBrowseUrl = "ckeditor/ckfinder/ckfinder.aspx?type=Flash";

    config.filebrowserUploadUrl = "ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files";
    config.filebrowserImageUploadUrl = "ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images";
    config.filebrowserFlashUploadUrl = "ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash";
    

    //Giữ nguyên định dạng khi copy từ word
    config.pasteFromWordRemoveFontStyles = false;
    config.pasteFromWordRemoveStyles = false;
    config.pasteFromWordNumberedHeadingToList = true;
    config.pasteFromWordPromptCleanup = true;

    // Cho phép giữ thuộc tính ID phục vụ gắn #hashtag
    config.extraAllowedContent = "*[id]";

    config.wordcount = {

        // Whether or not you want to show the Paragraphs Count
        showParagraphs: true,

        // Whether or not you want to show the Word Count
        showWordCount: true,

        // Whether or not you want to show the Char Count
        showCharCount: true,

        // Whether or not you want to count Spaces as Chars
        countSpacesAsChars: false,

        // Whether or not to include Html chars in the Char Count
        countHTML: false,

        // Maximum allowed Word Count, -1 is default for unlimited
        maxWordCount: -1,

        // Maximum allowed Char Count, -1 is default for unlimited
        maxCharCount: -1,

        // Add filter to add or remove element before counting (see CKEDITOR.htmlParser.filter), Default value : null (no filter)
        filter: new CKEDITOR.htmlParser.filter({
            elements: {
                div: function (element) {
                    if (element.attributes.class == "mediaembed") {
                        return false;
                    }
                }
            }
        })
    };
};