'use client'

import { getIndustriesAction } from '@/actions/industry.Actions'
import { Button } from '@/components/ui/button'
import { Command, CommandEmpty, CommandGroup, CommandInput, CommandItem, CommandList } from '@/components/ui/command'
import { Popover, PopoverContent, PopoverTrigger } from '@/components/ui/popover'
import { apiErrorHandler } from '@/lib/utils'
import { IndustryResponse } from '@/types/industry.types'
import { Dispatch, SetStateAction, useEffect, useState } from 'react'

type Props = {
  setIndustriesChoosed: Dispatch<SetStateAction<IndustryResponse[] | null>>
}

export default function IndustryComboboxPopover({ setIndustriesChoosed }: Props) {
  const [open, setOpen] = useState(false)
  const [industries, setIndustries] = useState<IndustryResponse[] | null>(null)

  useEffect(() => {
    getIndustriesAction('')
      .then((data) => {
        console.log('>>>', data)
        setIndustries(data.data)
      })
      .catch((error: Error) => {
        apiErrorHandler(error.message)
      })
  }, [])

  const handleSelect = (value: string) => {
    console.log('>>> value', value)
    const selectedIndustry = industries?.find((industry) => industry.id === value) || null

    setIndustriesChoosed((prev) => {
      if (prev === null) {
        return selectedIndustry ? [selectedIndustry] : null
      } else {
        if (selectedIndustry && !prev.some((industry) => industry.id === value)) {
          return [...prev, selectedIndustry]
        }
        return prev
      }
    })

    setOpen(false)
  }

  return (
    <div className='flex items-center space-x-4'>
      <Popover open={open} onOpenChange={setOpen}>
        <PopoverTrigger asChild>
          <Button variant='outline' className='w-[150px] justify-start'>
            + Thêm lĩnh vực
          </Button>
        </PopoverTrigger>
        <PopoverContent className='p-0' side='right' align='start'>
          <Command>
            <CommandInput placeholder='Tìm kiếm lĩnh vực' />
            <CommandList>
              <CommandEmpty>No results found.</CommandEmpty>
              <CommandGroup>
                {industries?.map((industry) => (
                  <CommandItem key={industry.id} value={industry.id} onSelect={(value) => handleSelect(value)}>
                    {industry.name}
                  </CommandItem>
                ))}
              </CommandGroup>
            </CommandList>
          </Command>
        </PopoverContent>
      </Popover>
    </div>
  )
}
