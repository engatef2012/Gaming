// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('select').select2();
$('select[multiple]').select2({
    closeOnSelect: false,
    allowClear: true,
    placeholder: 'Select supported devices'
});