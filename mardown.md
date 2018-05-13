# Markdown
$（1-6）是标题大小  
$ 两个空格是换行  
$ - 或者1. 2. 加一个空格是列表  
$ []()是链接  
$ ![]() 是图片  
$ > 是引用  
*斜体*  
**粗体**  
***分割  
=======换行  
[mardown]()https://stackedit.io/editor#fn:stackedit
# github 命令
克隆仓库 git clone https://github.com/libgit2/libgit2 name(name是本地的名字可以自定义)
GitHub 有一个十分详细的针对数十种项目及语言的 .gitignore 文件列表，你可以在 https://github.com/github/gitignore 找到它.
<hr/>
打开文件位置或者git仓库位置 cd  .. 上次层
<hr/>
git status 查看当前的git状态 -s --short 显示简略信息
<hr/>
git add [name] 添加文件监视
文件 .gitignore 的格式规范如下：  

所有空行或者以 ＃ 开头的行都会被 Git 忽略。  
可以使用标准的 glob 模式匹配。  
匹配模式可以以（/）开头防止递归。  
匹配模式可以以（/）结尾指定目录。  
要忽略指定模式以外的文件或目录，可以在模式前加上惊叹号（!）取反。 
所谓的 glob 模式是指 shell 所使用的简化了的正则表达式。星号（\*）匹配零个或多个任意字符；[abc] 匹配任何一个列在方括号中的字符（这个例子要么匹配一个 a，要么匹配一个 b，要么匹配一个 c）；问号（?）只匹配一个任意字符；如果在方括号中使用短划线分隔两个字符，表示所有在这两个字符范围内的都可以匹配（比如 [0-9] 表示匹配所有 0 到 9 的数字）。 使用两个星号（*) 表示匹配任意中间目录，比如a/**/z 可以匹配 a/z, a/b/z 或 a/b/c/z等。  
我们再看一个 .gitignore 文件的例子：  
>`# no .a files`  
>*.a  
>`# but do track lib.a, even though you're ignoring .a files above`  
>!lib.a  
>`# only ignore the TODO file in the current directory, not subdir/TODO`  
>/TODO  
>`# ignore all files in the build/ directory`  
>build/  
>`# ignore doc/notes.txt, but not doc/server/arch.txt`  
>doc/*.txt  
>`# ignore all .pdf files in the doc/ directory`  
>doc/**/*.pdf  

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
$ git remote add pb https://github.com/paulboone/ticgit 添加远程仓库
<hr/>
从远程仓库中抓取与拉取
$ git fetch [remote-name]
$ git push origin master
<hr/>
$ git tag 查看tag 可以直接加名字创建tag-l 'name'
<hr/>
git log --oneline --decorate
创建分支 
$ git branch testing
<hr/>切换分支
$ git checkout testing
<hr/>
$ git checkout -b iss533 
相当于
$ git branch iss53
$ git checkout iss53
<hr/>https://git-scm.com/book/zh/v2/Git-%E5%88%86%E6%94%AF-%E5%88%86%E6%94%AF%E7%9A%84%E6%96%B0%E5%BB%BA%E4%B8%8E%E5%90%88%E5%B9%B6
切换回主分支，合并从主分支分支出去的修改 hotfix
$ git checkout master
$ git merge hotfix
$ git branch -d hotfix 删除临时分支
<tr/>$ git ls-remote(remote)显式地获得远程引用的完整列表
$git remote show(remote)
$ git checkout -b serverfix origin/serverfix 基于远程分支创建新的分支
<hr/>$ git push origin --delete serverfix  --delete 选项的 git push 命令来删除一个远程分支
<hr/> ######变基 不要对在你的仓库外有副本的分支执行变基。
$ git checkout experiment
$ git rebase master
现在回到 master 分支，进行一次快进合并。
$ git checkout master
$ git merge experiment
$ git rebase --onto master server client
“取出 client 分支，找出处于 client 分支和 server 分支的共同祖先之后的修改，然后把它们在 master 分支上重演一遍”  
现在可以快进合并 master 分支了  
$ git checkout master
$ git merge client
