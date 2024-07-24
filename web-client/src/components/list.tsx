type props = {
  title: string
  list: string[]
}

export default function List({ title, list }: props) {
  return (
    <>
      <div>
        <h2 className='mb-2 text-lg font-semibold text-gray-900 dark:text-white'>{title}</h2>
        <ul className='max-w-md space-y-1 text-gray-500 list-inside dark:text-gray-400'>
          {list.length > 0 ? (
            list.map((ls, index) => (
              <li key={index} className='flex items-center'>
                <svg
                  className='w-3.5 h-3.5 me-2 text-green-500 dark:text-green-400 flex-shrink-0'
                  aria-hidden='true'
                  xmlns='http://www.w3.org/2000/svg'
                  fill='currentColor'
                  viewBox='0 0 20 20'
                >
                  <path d='M10 .5a9.5 9.5 0 1 0 9.5 9.5A9.51 9.51 0 0 0 10 .5Zm3.707 8.207-4 4a1 1 0 0 1-1.414 0l-2-2a1 1 0 0 1 1.414-1.414L9 10.586l3.293-3.293a1 1 0 0 1 1.414 1.414Z' />
                </svg>
                {ls}
              </li>
            ))
          ) : (
            <></>
          )}
        </ul>
      </div>
    </>
  )
}
