# jqm-tree
jqm-tree is a jquery mobile tree plugin<BR>
jqm-tree 是一个jquery mobile的树形插件

example(使用说明):<BR>
$("#tree").jqmtree({<BR>
　　title : 'Items',<BR>
　　collapsed: false,<BR>
　　data: [<BR>
　　　　{ "id": 1, "title": "item1" },<BR>
　　　　{ "id": 2, "title": "item1_1", "pid":1 },<BR>
　　　　{ "id": 3, "title": "item1_2", "pid": 1 },<BR>
　　　　{ "id": 4, "title": "item2", "pid": 0 },<BR>
　　　　{ "id": 5, "title": "item3", "pid": 0 },<BR>
　　　　{ "id": 6, "title": "item1_2_1", "pid": 3 }<BR>
　　]<BR>
});
