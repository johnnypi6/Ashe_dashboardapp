tinymce.init({
	selector: "textarea.tinymce",
	plugins: [
	    "advlist autolink lists link image charmap print preview anchor",
	    "searchreplace visualblocks code fullscreen",
	    "insertdatetime media table paste imagetools wordcount"
	],
  	toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
  	min_height: 400,
  	style_formats: [
        {title: 'Bold text', inline: 'b'},
        {title: 'Red text', inline: 'span', styles: {color: '#ff0000'}},
        {title: 'Red header', block: 'h1', styles: {color: '#ff0000'}},
        {title: 'Example 1', inline: 'span', classes: 'example1'},
        {title: 'Example 2', inline: 'span', classes: 'example2'},
        {title: 'Table styles'},
        {title: 'Table row 1', selector: 'tr', classes: 'tablerow1'}
    ]
});