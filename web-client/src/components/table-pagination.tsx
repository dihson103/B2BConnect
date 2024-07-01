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

export default function AppPagination({ currentPage, totalPages }: Props) {
  const [page, setPage] = useState<number>(currentPage)

  const pathname = usePathname()
  const router = useRouter()
  const searchParams = useSearchParams()
  const params = new URLSearchParams(searchParams)

  const handlePreviousClick = () => {
    const pageIndex = page - 1
    if (pageIndex == 0) return
    setPage(pageIndex)
    params.set('pageIndex', pageIndex.toString())
    router.push(`${pathname}?${params.toString()}`, { scroll: false })
  }

  const handleClick = (pageIndex: number) => () => {
    setPage(pageIndex)
    params.set('pageIndex', pageIndex.toString())
    router.push(`${pathname}?${params.toString()}`, { scroll: false })
  }

  const handleNextClick = () => {
    const pageIndex = page + 1
    if (pageIndex > totalPages) return
    setPage(pageIndex)
    params.set('pageIndex', pageIndex.toString())
    router.push(`${pathname}?${params.toString()}`, { scroll: false })
  }

  return (
    <Pagination>
      <PaginationContent>
        <PaginationItem className='hover:cursor-pointer'>
          <PaginationPrevious onClick={handlePreviousClick} />
        </PaginationItem>
        <PaginationItem className='hover:cursor-pointer'>
          <PaginationLink onClick={handleClick(1)}>1</PaginationLink>
        </PaginationItem>
        <PaginationItem className='hover:cursor-pointer'>
          <PaginationLink onClick={handleClick(2)}>2</PaginationLink>
        </PaginationItem>
        <PaginationItem>
          <PaginationEllipsis />
        </PaginationItem>
        <PaginationItem className='hover:cursor-pointer'>
          <PaginationNext onClick={handleNextClick} />
        </PaginationItem>
      </PaginationContent>
    </Pagination>
  )
}
