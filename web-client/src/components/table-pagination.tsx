'use client'

import {
  Pagination,
  PaginationContent,
  PaginationEllipsis,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious
} from '@/components/ui/pagination'
import { usePathname, useRouter, useSearchParams } from 'next/navigation'
import { useState } from 'react'

type Props = {
  currentPage: number
  totalPages: number
}

const RenderPageNumber = ({
  currentPage,
  totalPages,
  handleClick
}: {
  currentPage: number
  totalPages: number
  handleClick: (pageIndex: number) => () => void
}) => {
  if (totalPages <= 5) {
    return (
      <>
        {Array.from({ length: totalPages }, (_, i) => i + 1).map((pageIndex) => (
          <PaginationItem
            key={pageIndex}
            className={`hover:cursor-pointer ${pageIndex === currentPage ? 'bg-gray-200 dark:bg-muted/40 rounded' : ''}`}
          >
            <PaginationLink onClick={handleClick(pageIndex)}>{pageIndex}</PaginationLink>
          </PaginationItem>
        ))}
      </>
    )
  }

  if (currentPage <= 3) {
    return (
      <>
        {Array.from({ length: 5 }, (_, i) => i + 1).map((pageIndex) => (
          <PaginationItem
            key={pageIndex}
            className={`hover:cursor-pointer ${pageIndex === currentPage ? 'bg-gray-200 dark:bg-muted/40 rounded' : ''}`}
          >
            <PaginationLink onClick={handleClick(pageIndex)}>{pageIndex}</PaginationLink>
          </PaginationItem>
        ))}
        <PaginationItem>
          <PaginationEllipsis />
        </PaginationItem>
        <PaginationItem className='hover:cursor-pointer'>
          <PaginationLink onClick={handleClick(totalPages)}>{totalPages}</PaginationLink>
        </PaginationItem>
      </>
    )
  }

  if (currentPage > totalPages - 3) {
    return (
      <>
        <PaginationItem className='hover:cursor-pointer'>
          <PaginationLink onClick={handleClick(1)}>1</PaginationLink>
        </PaginationItem>
        <PaginationItem>
          <PaginationEllipsis />
        </PaginationItem>
        {Array.from({ length: 5 }, (_, i) => totalPages - 4 + i).map((pageIndex) => (
          <PaginationItem
            key={pageIndex}
            className={`hover:cursor-pointer ${pageIndex === currentPage ? 'bg-gray-200 dark:bg-muted/40 rounded' : ''}`}
          >
            <PaginationLink onClick={handleClick(pageIndex)}>{pageIndex}</PaginationLink>
          </PaginationItem>
        ))}
      </>
    )
  }

  return (
    <>
      <PaginationItem className='hover:cursor-pointer'>
        <PaginationLink onClick={handleClick(1)}>1</PaginationLink>
      </PaginationItem>
      <PaginationItem>
        <PaginationEllipsis />
      </PaginationItem>
      {Array.from({ length: 5 }, (_, i) => currentPage - 2 + i).map((pageIndex) => (
        <PaginationItem
          key={pageIndex}
          className={`hover:cursor-pointer ${pageIndex === currentPage ? 'bg-gray-200 dark:bg-muted/40 rounded' : ''}`}
        >
          <PaginationLink onClick={handleClick(pageIndex)}>{pageIndex}</PaginationLink>
        </PaginationItem>
      ))}
      <PaginationItem>
        <PaginationEllipsis />
      </PaginationItem>
      <PaginationItem className='hover:cursor-pointer'>
        <PaginationLink onClick={handleClick(totalPages)}>{totalPages}</PaginationLink>
      </PaginationItem>
    </>
  )
}

export default function AppPagination({ currentPage, totalPages }: Props) {
  const [page, setPage] = useState<number>(currentPage)

  const pathname = usePathname()
  const router = useRouter()
  const searchParams = useSearchParams()
  const params = new URLSearchParams(searchParams.toString())

  const handlePreviousClick = () => {
    if (page > 1) {
      const pageIndex = page - 1
      setPage(pageIndex)
      params.set('pageIndex', pageIndex.toString())
      router.push(`${pathname}?${params.toString()}`, { scroll: false })
    }
  }

  const handleClick = (pageIndex: number) => () => {
    if (pageIndex !== page) {
      setPage(pageIndex)
      params.set('pageIndex', pageIndex.toString())
      router.push(`${pathname}?${params.toString()}`, { scroll: false })
    }
  }

  const handleNextClick = () => {
    if (page < totalPages) {
      const pageIndex = page + 1
      setPage(pageIndex)
      params.set('pageIndex', pageIndex.toString())
      router.push(`${pathname}?${params.toString()}`, { scroll: false })
    }
  }

  return (
    <Pagination>
      <PaginationContent>
        <PaginationItem className={`hover:cursor-pointer ${page === 1 ? 'disabled' : ''}`}>
          <PaginationPrevious onClick={page === 1 ? undefined : handlePreviousClick} />
        </PaginationItem>
        <RenderPageNumber currentPage={page} totalPages={totalPages} handleClick={handleClick} />
        <PaginationItem className={`hover:cursor-pointer ${page === totalPages ? 'disabled' : ''}`}>
          <PaginationNext onClick={page === totalPages ? undefined : handleNextClick} />
        </PaginationItem>
      </PaginationContent>
    </Pagination>
  )
}
