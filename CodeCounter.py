import os
import tabulate

class CodeStatistics:
    def __init__(self):
        self.project_name = ""
        self.total_files = 0
        self.total_lines = 0
        self.total_blank_lines = 0
        self.total_code_lines = 0

file_extension = ".cs"
project_name_prefix = "DailyStatistics"
sub_folders_with_projects = [ "Tests", "View" ]

root_path = os.path.dirname(os.path.abspath(__file__))
folders_path = [f for f in os.listdir(root_path) if os.path.isdir(os.path.join(root_path, f)) and f.startswith(project_name_prefix)]

for sub_folder in sub_folders_with_projects:
    sub_folder_path = os.path.join(root_path, sub_folder)
    folders_path += [os.path.join(sub_folder, f) for f in os.listdir(sub_folder_path) if os.path.isdir(os.path.join(sub_folder_path, f)) and f.startswith(project_name_prefix)]

projects_statistics = []

for folder in folders_path:
    project_statistics = CodeStatistics()
    project_statistics.project_name = folder.split(os.sep)[-1]
    projects_statistics.append(project_statistics)

    project_path = os.path.join(root_path, folder)
    for root, dirs, files in os.walk(project_path):
        for file in files:
            if file.endswith(file_extension):
                project_statistics.total_files += 1
                file_path = os.path.join(root, file)
                with open(file_path, 'r') as f:
                    lines = f.readlines()
                    project_statistics.total_lines += len(lines)
                    project_statistics.total_blank_lines += len([l for l in lines if l.strip() == ''])
                    project_statistics.total_code_lines += len([l for l in lines if l.strip() != ''])


table = []
for project_statistics in projects_statistics:
    table.append([project_statistics.project_name, project_statistics.total_files, project_statistics.total_lines, project_statistics.total_blank_lines, project_statistics.total_code_lines])
table.append(["Total",
              sum([project_statistics.total_files for project_statistics in projects_statistics]),
              sum([project_statistics.total_lines for project_statistics in projects_statistics]),
              sum([project_statistics.total_blank_lines for project_statistics in projects_statistics]),
              sum([project_statistics.total_code_lines for project_statistics in projects_statistics])])

print(tabulate.tabulate(table, headers=["Project", "Files", "Lines", "Blank Lines", "Code Lines"], tablefmt="grid"))