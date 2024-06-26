import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from '@/components/ui/tooltip'
import { ReactNode } from 'react'

type Props = {
  children: ReactNode
  displayValue: string
}

export default function AppToolTip({ children, displayValue }: Props) {
  return (
    <TooltipProvider>
      <Tooltip>
        <TooltipTrigger asChild>{children}</TooltipTrigger>
        <TooltipContent side='right'>{displayValue}</TooltipContent>
      </Tooltip>
    </TooltipProvider>
  )
}
