# github 命令
克隆仓库 git clone https://github.com/libgit2/libgit2 name(name是本地的名字可以自定义)
<hr/>
打开文件位置或者git仓库位置 cd  .. 上次层
<hr/>
git status 查看当前的git状态 -s --short 显示简略信息
<hr/>
git add [name] 添加文件监视
<hr/>
git commit -m "Story 182: Fix benchmarks for speed"
<hr/>
git commit -a -m 'added new benchmarks' 跳过暂存的步骤直接commit
<hr/?
rm PROJECTS.md  先删除   git rm 删除文件记录 -f强制删除
<hr/>
$ git rm --cached README 删除之后还存在
git rm 命令后面可以列出文件或者目录的名字，也可以使用 glob 模式。 比方说：
$ git rm log/\*.log
注意到星号 * 之前的反斜杠 \， 因为 Git 有它自己的文件模式扩展匹配方式，所以我们不用 shell 来帮忙展开。 此命令删除 log/ 目录下扩展名为 .log 的所有文件。 类似的比如：
$ git rm \*~
该命令为删除以 ~ 结尾的所有文件。
<hr/>
$ git mv file_from file_to 
相当于
$ mv README.md README
$ git rm README.md
$ git add README
<hr/>
-S，可以列出那些添加或移除了某些字符串的提交。 比如说，你想找出添加或移除了某一个特定函数的引用的提交，你可以这样使用：
$ git log -Sfunction_name
<hr/>
查看 Git 仓库中，2008 年 10 月期间，Junio Hamano 提交的但未合并的测试文件，可以用下面的查询命令：
$ git log --pretty="%h - %s" --author=gitster --since="2008-10-01" \
<hr/>
$ git remote -v 查看远程仓库 -v显示对应的url
<hr/>
从远程仓库中抓取与拉取
$ git fetch [remote-name]