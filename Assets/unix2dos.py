import os
import glob
import sys

abs_name = sys.argv[0]
print("abs_name:" + abs_name)
print("dirname:" + os.path.dirname(abs_name))
dirname = os.path.dirname(abs_name)
input("接下来是glob输出")
files = glob.glob(dirname + '/**/*.cs',recursive=True)

for file in files:
    print(file)
input("接下来开始转化")
for file in files:
    os.system("unix2dos " + file)
input("完成")
