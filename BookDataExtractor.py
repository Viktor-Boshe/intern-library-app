import pandas as pd

csv_file = 'books.csv'

columns_to_extract = ['title', 'author', 'description', 'coverImg', 'price']

df = pd.read_csv(csv_file, usecols=columns_to_extract)

df.to_csv('extracted_books.csv', index=False)