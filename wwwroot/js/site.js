// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Nav bar Dropdown Starts from here
$.fn.dropdown = (function () {
    var $bsDropdown = $.fn.dropdown;
    return function (config) {
        if (typeof config === 'string' && config === 'toggle') { // dropdown toggle trigged
            $('.has-child-dropdown-show').removeClass('has-child-dropdown-show');
            $(this).closest('.dropdown').parents('.dropdown').addClass('has-child-dropdown-show');
        }
        var ret = $bsDropdown.call($(this), config);
        $(this).off('click.bs.dropdown'); // Turn off dropdown.js click event, it will call 'this.toggle()' internal
        return ret;
    }
})();

$(function () {
    $('.dropdown [data-toggle="dropdown"]').on('click', function (e) {
        $(this).dropdown('toggle');
        e.stopPropagation(); // do not fire dropdown.js click event, it will call 'this.toggle()' internal
    });
    $('.dropdown').on('hide.bs.dropdown', function (e) {
        if ($(this).is('.has-child-dropdown-show')) {
            $(this).removeClass('has-child-dropdown-show');
            e.preventDefault();
        }
        e.stopPropagation();    // do not need pop in multi level mode
    });
});

// for hover
$('.dropdown-hover').on('mouseenter', function () {
    if (!$(this).hasClass('show')) {
        $('>[data-toggle="dropdown"]', this).dropdown('toggle');
    }
});
$('.dropdown-hover').on('mouseleave', function () {
    if ($(this).hasClass('show')) {
        $('>[data-toggle="dropdown"]', this).dropdown('toggle');
    }
});
$('.dropdown-hover-all').on('mouseenter', '.dropdown', function () {
    if (!$(this).hasClass('show')) {
        $('>[data-toggle="dropdown"]', this).dropdown('toggle');
    }
});
$('.dropdown-hover-all').on('mouseleave', '.dropdown', function () {
    if ($(this).hasClass('show')) {
        $('>[data-toggle="dropdown"]', this).dropdown('toggle');
    }
});

//Nav bar Dropdown Ends


//Admin Category Datatable Starts
$(document).ready(function () {
    $('#admin_category_table').DataTable({
        initComplete: function () {
            this.api().columns([0,1]).every(function () {
                var column = this;
                var select = $('<select><option value=""></option></select>')
                    .appendTo($(column.footer()).empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );

                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });
        }
    });
});
//Admin Category Datatable Ends

//Admin MiniCategory Datatable Starts
$(document).ready(function () {
    $('#admin_minicategory_table').DataTable({
        initComplete: function () {
            this.api().columns([0, 1, 2]).every(function () {
                var column = this;
                var select = $('<select><option value=""></option></select>')
                    .appendTo($(column.footer()).empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );

                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });
        }
    });
});
//Admin MiniCategory Datatable Ends


//Admin ProductItem Datatable Starts
$(document).ready(function () {
    $('#admin_product_table').DataTable({
        initComplete: function () {
            this.api().columns([0, 1, 2, 3]).every(function () {
                var column = this;
                var select = $('<select><option value=""></option></select>')
                    .appendTo($(column.footer()).empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );

                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });
        }
    });
});
//Admin ProductItem Datatable Ends